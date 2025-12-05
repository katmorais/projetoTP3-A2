using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using projetoTP3_A2.Data;
using projetoTP3_A2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projetoTP3_A2.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class PatologiaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PatologiaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Patologia
        public async Task<IActionResult> Index()
        {
            return View(await _context.Patologia.ToListAsync());
        }

        // GET: Patologia/Details/5
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

        // GET: Patologia/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Patologia/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
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

        // GET: Patologia/Edit/5
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

        // POST: Patologia/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
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

        // GET: Patologia/Delete/5
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

        // POST: Patologia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
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
