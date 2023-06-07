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
    public class DepartamentosController : Controller
    {
        private readonly rrhhContext _context;

        public DepartamentosController(rrhhContext context)
        {
            _context = context;
        }

        [Authorize("RequireAdminRole")]
        // GET: departamentoes
        public async Task<IActionResult> Index()
        {
              return View(await _context.departamentos.ToListAsync());
        }

        // GET: departamentoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.departamentos == null)
            {
                return NotFound();
            }

            var departamento = await _context.departamentos
                .FirstOrDefaultAsync(m => m.id == id);
            if (departamento == null)
            {
                return NotFound();
            }

            return View(departamento);
        }

        // GET: departamentoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: departamentoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,departamento1")] departamento departamento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(departamento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(departamento);
        }

        // GET: departamentoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.departamentos == null)
            {
                return NotFound();
            }

            var departamento = await _context.departamentos.FindAsync(id);
            if (departamento == null)
            {
                return NotFound();
            }
            return View(departamento);
        }

        // POST: departamentoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,departamento1")] departamento departamento)
        {
            if (id != departamento.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(departamento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!departamentoExists(departamento.id))
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
            return View(departamento);
        }

        // GET: departamentoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.departamentos == null)
            {
                return NotFound();
            }

            var departamento = await _context.departamentos
                .FirstOrDefaultAsync(m => m.id == id);
            if (departamento == null)
            {
                return NotFound();
            }

            return View(departamento);
        }

        // POST: departamentoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.departamentos == null)
            {
                return Problem("Entity set 'rrhhContext.departamentos'  is null.");
            }
            var departamento = await _context.departamentos.FindAsync(id);
            if (departamento != null)
            {
                _context.departamentos.Remove(departamento);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool departamentoExists(int id)
        {
          return _context.departamentos.Any(e => e.id == id);
        }
    }
}
