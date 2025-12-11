using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;    
using projetoTP3_A2.Data;
using projetoTP3_A2.Models;
using projetoTP3_A2.Models.Enum;
using projetoTP3_A2.Models.ViewModels;

namespace projetoTP3_A2.Controllers
{
    [Authorize(Roles = "Medico,Administrador")]
    public class MedicoController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public MedicoController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public IActionResult Pacientes(string query, int page = 1, int pageSize = 10)
        {
            var pacientes = _userManager.Users.Where(u => u.Perfil == Perfis.Paciente);

            if (!string.IsNullOrWhiteSpace(query))
            {
                pacientes = pacientes.Where(u => u.Nome.Contains(query));
            }

            var totalCount = pacientes.Count();

            var lista = pacientes
                .OrderBy(u => u.Nome)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(u => new PacienteListItem { Id = u.Id, Nome = u.Nome, Email = u.Email })
                .ToList();

            ViewBag.TotalCount = totalCount;
            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;

            return View(lista);
        }

        public async Task<IActionResult> Prontuario(Guid id)
        {
            var paciente = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            
            if (paciente == null) return NotFound();

            var historico = await _context.Prescricoes
                .Include(p => p.Medico)
                .Include(p => p.Itens)
                    .ThenInclude(i => i.Medicamento)
                .Where(p => p.PacienteId == id)
                .OrderByDescending(p => p.DataPrescricao)
                .ToListAsync();

            var viewModel = new ProntuarioViewModel
            {
                Paciente = paciente,
                HistoricoPrescricoes = historico
            };

            return View(viewModel);
        }
    }

    public class PacienteListItem
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
    }
}