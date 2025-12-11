using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using projetoTP3_A2.Data;
using projetoTP3_A2.Models;
using projetoTP3_A2.Models.Enum;
using projetoTP3_A2.Models.ViewModels;
using projetoTP3_A2.Services.Interfaces;

namespace projetoTP3_A2.Controllers
{
    [Authorize]
    public class PrescricaoController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IOpenFdaService _fdaService;

        public PrescricaoController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IOpenFdaService fdaService)
        {
            _context = context;
            _userManager = userManager;
            _fdaService = fdaService;
        }

        [Authorize(Roles = "Medico,Administrador,Farmaceutico,Paciente")]
        public async Task<IActionResult> Index()
        {
            var prescricoes = await _context.Prescricoes
                .Include(p => p.Paciente)
                .Include(p => p.Medico)
                .OrderByDescending(p => p.DataPrescricao)
                .ToListAsync();

            return View(prescricoes);
        }

        [Authorize(Roles = "Medico")]
        public IActionResult Create()
        {
            var listaPacientes = _context.Users
                .Where(u => u.Perfil == Perfis.Paciente)
                .OrderBy(u => u.Nome)
                .ToList();

            ViewData["PacienteId"] = new SelectList(listaPacientes, "Id", "Nome");
            ViewData["MedicamentoId"] = new SelectList(_context.Medicamento, "Id", "Nome");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Medico")]
        public async Task<IActionResult> Create(PrescricaoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                RecarregarListas();
                return View(model);
            }

            var usuarioLogado = await _userManager.GetUserAsync(User);
            var medicamentoNovo = await _context.Medicamento.FindAsync(model.MedicamentoId);

            var historicoRemedios = await _context.ItensPrescricao
                .Include(i => i.Medicamento)
                .Include(i => i.Prescricao)
                .Where(i => i.Prescricao.PacienteId == model.PacienteId
                            && i.Prescricao.Status != PrescricaoStatus.Cancelado)
                .Select(i => i.Medicamento.Nome)
                .Distinct()
                .ToListAsync();

            bool houveInteracao = false;
            List<string> alertas = new List<string>();

            foreach (var remedioEmUso in historicoRemedios)
            {
                bool perigo = await _fdaService.VerificarInteracaoAsync(medicamentoNovo.Nome, remedioEmUso);

                if (perigo)
                {
                    houveInteracao = true;
                    alertas.Add($"ALERTA GRAVE: O medicamento '{medicamentoNovo.Nome}' possui interações registradas com '{remedioEmUso}', que o paciente já utiliza.");
                }
            }

            if (houveInteracao)
            {
                foreach (var alerta in alertas)
                {
                    ModelState.AddModelError(string.Empty, alerta);
                }
                model.AlertaInteracao = "Risco de interação medicamentosa detectado! Verifique os erros acima.";
                RecarregarListas();
                return View(model);
            }

            var prescricao = new Prescricao
            {
                Id = Guid.NewGuid(),
                PacienteId = model.PacienteId,
                MedicoId = usuarioLogado.Id,
                DataPrescricao = DateTime.UtcNow,
                DataValidade = DateTime.UtcNow.AddDays(30),
                Status = PrescricaoStatus.Pendente
            };

            var item = new ItemPrescricao
            {
                Id = Guid.NewGuid(),
                MedicamentoId = model.MedicamentoId,
                Quantidade = model.Quantidade,
                InstrucoesUso = model.Instrucoes,
                PrescricaoId = prescricao.Id
            };

            _context.Add(prescricao);
            _context.Add(item);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Medico,Administrador,Farmaceutico,Paciente")]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prescricao = await _context.Prescricoes
                .Include(p => p.Paciente)
                .Include(p => p.Medico)
                .Include(p => p.Itens)
                    .ThenInclude(i => i.Medicamento)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (prescricao == null)
            {
                return NotFound();
            }

            return View(prescricao);
        }

        private void RecarregarListas()
        {
            var listaPacientes = _context.Users
                .Where(u => u.Perfil == Perfis.Paciente)
                .OrderBy(u => u.Nome)
                .ToList();

            ViewData["PacienteId"] = new SelectList(listaPacientes, "Id", "Nome");
            ViewData["MedicamentoId"] = new SelectList(_context.Medicamento, "Id", "Nome");
        }
    }
}