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
    public class CandidatosController : Controller
    {
        private readonly rrhhContext _context;

        public CandidatosController(rrhhContext context)
        {
            _context = context;
        }

        [Authorize("RequireAdminRole")]
        // GET: Candidatos
        public async Task<IActionResult> Index()
        {
            var rrhhContext = _context.candidatos
                .Include(c => c.competencia)
                .Include(c => c.idiomas)
                .Include(c => c.departamentoNavigation)
                .Include(c => c.puestoaspiraNavigation);
            return View(await rrhhContext.ToListAsync());
        }

        [HttpGet]
        [Authorize("RequireAdminRole")]
        public async Task<IActionResult> Index(string Candsearch)
        {
            ViewData["candidatos"] = Candsearch;

            var candquery = await _context.candidatos
                .Include(c => c.competencia)
                .Include(c => c.idiomas)
                .Include(c => c.capacitaciones)
                .Include(c => c.departamentoNavigation)
                .Include(c => c.puestoaspiraNavigation).ToListAsync();

            if (candquery.Count < 1)
                return View(candquery);

            var competencias = candquery[0].competencia.ToString();
            var capacitaciones = candquery[0].capacitaciones.ToString();

            if (!string.IsNullOrEmpty(Candsearch))
            {
                candquery = candquery.Where(x => (x.cedula.Contains(Candsearch)) ||
                     x.competencia.Any(c => c.descripcion.ToLower().Trim().Contains(Candsearch.ToLower().Trim())) ||
                     x.capacitaciones.Any(c => c.descripcion.ToLower().Trim().Contains(Candsearch.ToLower().Trim())))
                    .ToList();
            }

            return View(candquery);
        }

        [Authorize("RequireAdminRole")]
        // GET: Candidatos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.candidatos == null)
            {
                return NotFound();
            }

            var candidato = await _context.candidatos
                .Include(c => c.competencia)
                .Include(c => c.idiomas)
                .Include(c => c.departamentoNavigation)
                .Include(c => c.puestoaspiraNavigation)
                .FirstOrDefaultAsync(m => m.id == id);
            if (candidato == null)
            {
                return NotFound();
            }

            return View(candidato);
        }

        // GET: Candidatos/Create
        public IActionResult Create()
        {
            ViewData["competencias"] = new SelectList(_context.competencias, "id", "descripcion");
            ViewData["idiomas"] = new SelectList(_context.idiomas, "id", "nombre");
            ViewData["departamento"] = new SelectList(_context.departamentos, "departamento1", "departamento1");
            ViewData["puestoaspira"] = new SelectList(_context.puestos, "nombre", "nombre");
            return View();
        }

        // POST: Candidatos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,cedula,nombre,puestoaspira,departamento,salarioaspira,explaboral,empresa,puestoocupado,fechadesde,fechahasta,salario,recomendadopor")] candidato candidato)
        {
            var fechadesde = Request.Form["fechadesde"][0];
            candidato.fechadesde = DateOnly.Parse(fechadesde);

            var fechahasta = Request.Form["fechahasta"][0];
            candidato.fechahasta = DateOnly.Parse(fechahasta);

            if (ModelState.IsValid)
            {
                var competenciasIds = Request.Form["competencia"].Select(x => int.Parse(x)).ToArray();
                var competencias = _context.competencias.Where(x => competenciasIds.Contains(x.id)).ToList();
                candidato.competencia = competencias;

                var idiomasIds = Request.Form["idiomas"].Select(x => int.Parse(x)).ToArray();
                var idiomas = _context.idiomas.Where(x => idiomasIds.Contains(x.id)).ToList();
                candidato.idiomas = idiomas;

                _context.Add(candidato);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["competencias"] = new SelectList(_context.competencias, "id", "descripcion", candidato.competencia);
            ViewData["idiomas"] = new SelectList(_context.idiomas, "id", "nombre", candidato.idiomas);
            ViewData["departamento"] = new SelectList(_context.departamentos, "departamento1", "departamento1", candidato.departamento);
            ViewData["puestoaspira"] = new SelectList(_context.puestos, "nombre", "nombre", candidato.puestoaspira);
            return View(candidato);
        }

        [Authorize("RequireAdminRole")]
        // GET: Candidatos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.candidatos == null)
            {
                return NotFound();
            }

            var candidato = await _context.candidatos.FindAsync(id);
            if (candidato == null)
            {
                return NotFound();
            }
            ViewData["competencias"] = new SelectList(_context.competencias, "id", "descripcion", candidato.competencia);
            ViewData["idiomas"] = new SelectList(_context.idiomas, "id", "nombre", candidato.idiomas);
            ViewData["departamento"] = new SelectList(_context.departamentos, "departamento1", "departamento1", candidato.departamento);
            ViewData["puestoaspira"] = new SelectList(_context.puestos, "nombre", "nombre", candidato.puestoaspira);
            return View(candidato);
        }

        // POST: Candidatos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize("RequireAdminRole")]
        public async Task<IActionResult> Edit(int id, [Bind("id,cedula,nombre,puestoaspira,departamento,salarioaspira,explaboral,empresa,puestoocupado,fechadesde,fechahasta,salario,recomendadopor")] candidato candidato)
        {
            if (id != candidato.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(candidato);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!candidatoExists(candidato.id))
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
            ViewData["competencias"] = new SelectList(_context.competencias, "id", "descripcion", candidato.competencia);
            ViewData["idiomas"] = new SelectList(_context.idiomas, "id", "nombre", candidato.idiomas);
            ViewData["departamento"] = new SelectList(_context.departamentos, "departamento1", "departamento1", candidato.departamento);
            ViewData["puestoaspira"] = new SelectList(_context.puestos, "nombre", "nombre", candidato.puestoaspira);
            return View(candidato);
        }

        [Authorize("RequireAdminRole")]
        // GET: Candidatos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.candidatos == null)
            {
                return NotFound();
            }

            var candidato = await _context.candidatos
                .Include(c => c.competencia)
                .Include(c => c.idiomas)
                .Include(c => c.departamentoNavigation)
                .Include(c => c.puestoaspiraNavigation)
                .FirstOrDefaultAsync(m => m.id == id);
            if (candidato == null)
            {
                return NotFound();
            }

            return View(candidato);
        }

        // POST: Candidatos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize("RequireAdminRole")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.candidatos == null)
            {
                return Problem("Entity set 'rrhhContext.candidatos'  is null.");
            }
            var candidato = await _context.candidatos.FindAsync(id);
            if (candidato != null)
            {
                _context.candidatos.Remove(candidato);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool candidatoExists(int id)
        {
          return _context.candidatos.Any(e => e.id == id);
        }
    }
}
