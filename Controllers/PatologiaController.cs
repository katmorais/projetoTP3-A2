using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projetoTP3_A2.Data;
using projetoTP3_A2.Models;

namespace projetoTP3_A2.Controllers
{
    [Authorize]
    public class PatologiaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PatologiaController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Administrador,Medico,Farmaceutico")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Patologia.ToListAsync());
        }

        [Authorize(Roles = "Administrador,Medico,Farmaceutico")]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patologia = await _context.Patologia
                .FirstOrDefaultAsync(m => m.Id == id);
            if (patologia == null)
            {
                return NotFound();
            }

            return View(patologia);
        }

        [Authorize(Roles = "Administrador")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Create([Bind("Id,Nome,Descricao")] Patologia patologia)
        {
            if (ModelState.IsValid)
            {
                patologia.Id = Guid.NewGuid();
                _context.Add(patologia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(patologia);
        }

        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patologia = await _context.Patologia.FindAsync(id);
            if (patologia == null)
            {
                return NotFound();
            }
            return View(patologia);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Nome,Descricao")] Patologia patologia)
        {
            if (id != patologia.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(patologia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatologiaExists(patologia.Id))
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
            return View(patologia);
        }

        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patologia = await _context.Patologia
                .FirstOrDefaultAsync(m => m.Id == id);
            if (patologia == null)
            {
                return NotFound();
            }

            return View(patologia);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var patologia = await _context.Patologia.FindAsync(id);
            if (patologia != null)
            {
                _context.Patologia.Remove(patologia);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PatologiaExists(Guid id)
        {
            return _context.Patologia.Any(e => e.Id == id);
        }
    }
}