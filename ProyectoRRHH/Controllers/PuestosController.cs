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
    public class PuestosController : Controller
    {
        private readonly rrhhContext _context;

        public PuestosController(rrhhContext context)
        {
            _context = context;
        }

        [Authorize("RequireAdminRole")]
        // GET: puestoes
        public async Task<IActionResult> Index()
        {
              return View(await _context.puestos.ToListAsync());
        }

        // GET: puestoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.puestos == null)
            {
                return NotFound();
            }

            var puesto = await _context.puestos
                .FirstOrDefaultAsync(m => m.id == id);
            if (puesto == null)
            {
                return NotFound();
            }

            return View(puesto);
        }

        // GET: puestoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: puestoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,nombre,nivelriesgo,salariomin,salariomax,estado")] puesto puesto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(puesto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(puesto);
        }

        // GET: puestoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.puestos == null)
            {
                return NotFound();
            }

            var puesto = await _context.puestos.FindAsync(id);
            if (puesto == null)
            {
                return NotFound();
            }
            return View(puesto);
        }

        // POST: puestoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,nombre,nivelriesgo,salariomin,salariomax,estado")] puesto puesto)
        {
            if (id != puesto.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(puesto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!puestoExists(puesto.id))
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
            return View(puesto);
        }

        // GET: puestoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.puestos == null)
            {
                return NotFound();
            }

            var puesto = await _context.puestos
                .FirstOrDefaultAsync(m => m.id == id);
            if (puesto == null)
            {
                return NotFound();
            }

            return View(puesto);
        }

        // POST: puestoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.puestos == null)
            {
                return Problem("Entity set 'rrhhContext.puestos'  is null.");
            }
            var puesto = await _context.puestos.FindAsync(id);
            if (puesto != null)
            {
                _context.puestos.Remove(puesto);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool puestoExists(int id)
        {
          return _context.puestos.Any(e => e.id == id);
        }
    }
}
