using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoRRHH.Models;

namespace ProyectoRRHH.Controllers
{
    public class idiomasController : Controller
    {
        private readonly rrhhContext _context;

        public idiomasController(rrhhContext context)
        {
            _context = context;
        }

        [Authorize("RequireAdminRole")]
        // GET: idiomas
        public async Task<IActionResult> Index()
        {
              return View(await _context.idiomas.ToListAsync());
        }

        // GET: idiomas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.idiomas == null)
            {
                return NotFound();
            }

            var idioma = await _context.idiomas
                .FirstOrDefaultAsync(m => m.id == id);
            if (idioma == null)
            {
                return NotFound();
            }

            return View(idioma);
        }

        // GET: idiomas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: idiomas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,nombre,nivel")] idioma idioma)
        {
            if (ModelState.IsValid)
            {
                _context.Add(idioma);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(idioma);
        }

        // GET: idiomas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.idiomas == null)
            {
                return NotFound();
            }

            var idioma = await _context.idiomas.FindAsync(id);
            if (idioma == null)
            {
                return NotFound();
            }
            return View(idioma);
        }

        // POST: idiomas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,nombre,nivel")] idioma idioma)
        {
            if (id != idioma.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(idioma);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!idiomaExists(idioma.id))
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
            return View(idioma);
        }

        // GET: idiomas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.idiomas == null)
            {
                return NotFound();
            }

            var idioma = await _context.idiomas
                .FirstOrDefaultAsync(m => m.id == id);
            if (idioma == null)
            {
                return NotFound();
            }

            return View(idioma);
        }

        // POST: idiomas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.idiomas == null)
            {
                return Problem("Entity set 'rrhhContext.idiomas'  is null.");
            }
            var idioma = await _context.idiomas.FindAsync(id);
            if (idioma != null)
            {
                _context.idiomas.Remove(idioma);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool idiomaExists(int id)
        {
          return _context.idiomas.Any(e => e.id == id);
        }
    }
}
