using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projetoTP3_A2.Data;
using projetoTP3_A2.Models;
using projetoTP3_A2.Services.Interfaces;

namespace projetoTP3_A2.Controllers
{
    [Authorize]
    public class FarmaciaController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IViaCepService _viaCepService;

        public FarmaciaController(ApplicationDbContext context, IViaCepService viaCepService)
        {
            _context = context;
            _viaCepService = viaCepService;
        }

        [Authorize(Roles = "Administrador,Medico,Farmaceutico,Paciente")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Farmacia.ToListAsync());
        }

        [Authorize(Roles = "Administrador,Medico,Farmaceutico,Paciente")]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var farmacia = await _context.Farmacia
                .FirstOrDefaultAsync(m => m.Id == id);
            if (farmacia == null)
            {
                return NotFound();
            }

            return View(farmacia);
        }

        [Authorize(Roles = "Administrador")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Create([Bind("Id,Nome,Cep,Numero,Complemento")] Farmacia farmacia)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(farmacia.Cep))
                {
                    var endereco = await _viaCepService.BuscarEnderecoPorCepAsync(farmacia.Cep);
                    if (endereco != null)
                    {
                        farmacia.Logradouro = endereco.Logradouro;
                        farmacia.Bairro = endereco.Bairro;
                        farmacia.Localidade = endereco.Localidade;
                        farmacia.Uf = endereco.Uf;
                    }
                    else
                    {
                        ModelState.AddModelError("Cep", "CEP não encontrado.");
                        return View(farmacia);
                    }
                }

                farmacia.Id = Guid.NewGuid();
                _context.Add(farmacia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(farmacia);
        }

        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var farmacia = await _context.Farmacia.FindAsync(id);
            if (farmacia == null)
            {
                return NotFound();
            }
            return View(farmacia);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Nome,Cep,Numero,Complemento")] Farmacia farmacia)
        {
            if (id != farmacia.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (!string.IsNullOrEmpty(farmacia.Cep))
                    {
                        var endereco = await _viaCepService.BuscarEnderecoPorCepAsync(farmacia.Cep);
                        if (endereco != null)
                        {
                            farmacia.Logradouro = endereco.Logradouro;
                            farmacia.Bairro = endereco.Bairro;
                            farmacia.Localidade = endereco.Localidade;
                            farmacia.Uf = endereco.Uf;
                        }
                    }

                    _context.Update(farmacia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FarmaciaExists(farmacia.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(farmacia);
        }

        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var farmacia = await _context.Farmacia
                .FirstOrDefaultAsync(m => m.Id == id);
            if (farmacia == null)
            {
                return NotFound();
            }

            return View(farmacia);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var farmacia = await _context.Farmacia.FindAsync(id);
            if (farmacia != null)
            {
                _context.Farmacia.Remove(farmacia);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FarmaciaExists(Guid id)
        {
            return _context.Farmacia.Any(e => e.Id == id);
        }
    }
}