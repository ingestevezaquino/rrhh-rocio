using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.InkML;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using ProyectoRRHH.Models;

namespace ProyectoRRHH.Controllers
{
    public class EmpleadosController : Controller
    {
        private readonly rrhhContext _context;

        public EmpleadosController(rrhhContext context)
        {
            _context = context;
        }

        [Authorize("RequireAdminRole")]
        // GET: Empleados
        public async Task<IActionResult> Index()
        {
            var rrhhContext = _context.empleados
                .Include(e => e.cedulaNavigation)
                .Include(e => e.departamentoNavigation)
                .Include(e => e.puestoNavigation);
            return View(await rrhhContext.ToListAsync());
        }

        //View reporte
        [HttpGet]
        public async Task<IActionResult> Excel()
        {
            var rrhhContext = _context.empleados
                .Include(e => e.cedulaNavigation)
                .Include(e => e.departamentoNavigation)
                .Include(e => e.puestoNavigation);
            return View(await rrhhContext.ToListAsync());
        }

        //Exportar Empleados Excel
        [HttpPost]
        public async Task<FileResult> ExportarEmpleadosAExcel([FromQuery] string fechaInicio, [FromQuery] string fechaFin)
        {
            var fechaInicioDate = DateOnly.Parse(fechaInicio);
            var fechaFinDate = DateOnly.Parse(fechaFin);

            /*var tst = Request;
            var test = Request.Form["fechaInicio"];
            var fechaInicioDate = DateOnly.Parse(Request.Form["fechaInicio"][0]);
            var fechaFinDate = DateOnly.Parse(Request.Form["fechaFin"][0]);*/

            var empleados = await _context.empleados
                .Where(e => e.fechaingreso >= fechaInicioDate &&  e.fechaingreso <= fechaFinDate)
                .ToListAsync();

            var nombreArchivo = $"Empleados.xlsx";
            return GenerarExcel(nombreArchivo, empleados);
        }

        //EXCEL
        private FileResult GenerarExcel(string nameArchive, IEnumerable<empleado> empleados)
        {
            DataTable dataTable = new DataTable("empleado");
            dataTable.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("Cedula"),
                new DataColumn("Nombre"),
                new DataColumn("Fecha_Ingreso"),
                new DataColumn("Departamento"),
                new DataColumn("Puesto"),
                new DataColumn("Salario_Mensual"),
                new DataColumn("Estado")
            });

            foreach (var empleado in empleados)
            {
                var row = dataTable.NewRow();
                row["Cedula"] = empleado.cedula;
                row["Nombre"] = empleado.nombre;
                row["Fecha_Ingreso"] = ((DateOnly)empleado.fechaingreso).ToString("dd/MM/yyyy");
                row["Departamento"] = empleado.departamento;
                row["Puesto"] = empleado.puesto;
                row["Salario_Mensual"] = empleado.salariomensual;
                row["Estado"] = empleado.estado;

                dataTable.Rows.Add(row);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dataTable);

                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        nameArchive);
                }

            }

        }
            // GET: Empleados/Details/5
            public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.empleados == null)
            {
                return NotFound();
            }

            var empleado = await _context.empleados
                .Include(e => e.cedulaNavigation)
                .Include(e => e.departamentoNavigation)
                .Include(e => e.puestoNavigation)
                .FirstOrDefaultAsync(m => m.id == id);
            if (empleado == null)
            {
                return NotFound();
            }

            return View(empleado);
        }

        // GET: Empleados/Create
        public IActionResult Create()
        {
            ViewData["cedula"] = new SelectList(_context.candidatos, "cedula", "cedula");
            ViewData["departamento"] = new SelectList(_context.departamentos, "departamento1", "departamento1");
            ViewData["puesto"] = new SelectList(_context.puestos, "nombre", "nombre");
            return View();
        }

        // POST: Empleados/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,cedula,nombre,fechaingreso,departamento,puesto,salariomensual,estado")] empleado empleado)
        {
            var fechaingreso = Request.Form["fechaingreso"][0];
            empleado.fechaingreso = DateOnly.Parse(fechaingreso);

            var opciones = new List<SelectListItem>
                {
                    new SelectListItem { Value = bool.TrueString, Text = "Activo" },
                    new SelectListItem { Value = bool.FalseString, Text = "Inactivo" }
                };

            ViewBag.estado = opciones;

            if (ModelState.IsValid)
            {
                _context.Add(empleado);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["cedula"] = new SelectList(_context.candidatos, "cedula", "cedula", empleado.cedula);
            ViewData["departamento"] = new SelectList(_context.departamentos, "departamento1", "departamento1", empleado.departamento);
            ViewData["puesto"] = new SelectList(_context.puestos, "nombre", "nombre", empleado.puesto);
            ViewBag.estado = new SelectList(opciones, "Value", "Text", empleado.estado.ToString());
            return View(empleado);
        }
        // GET: Empleados/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.empleados == null)
            {
                return NotFound();
            }

            var empleado = await _context.empleados.FindAsync(id);
            if (empleado == null)
            {
                return NotFound();
            }
            ViewData["cedula"] = new SelectList(_context.candidatos, "cedula", "cedula", empleado.cedula);
            ViewData["departamento"] = new SelectList(_context.departamentos, "departamento1", "departamento1", empleado.departamento);
            ViewData["puesto"] = new SelectList(_context.puestos, "nombre", "nombre", empleado.puesto);
            return View(empleado);
        }

        // POST: Empleados/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,cedula,nombre,fechaingreso,departamento,puesto,salariomensual,estado")] empleado empleado)
        {
            if (id != empleado.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(empleado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!empleadoExists(empleado.id))
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
            ViewData["cedula"] = new SelectList(_context.candidatos, "cedula", "cedula", empleado.cedula);
            ViewData["departamento"] = new SelectList(_context.departamentos, "departamento1", "departamento1", empleado.departamento);
            ViewData["puesto"] = new SelectList(_context.puestos, "nombre", "nombre", empleado.puesto);
            return View(empleado);
        }

        // GET: Empleados/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.empleados == null)
            {
                return NotFound();
            }

            var empleado = await _context.empleados
                .Include(e => e.cedulaNavigation)
                .Include(e => e.departamentoNavigation)
                .Include(e => e.puestoNavigation)
                .FirstOrDefaultAsync(m => m.id == id);
            if (empleado == null)
            {
                return NotFound();
            }

            return View(empleado);
        }

        // POST: Empleados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.empleados == null)
            {
                return Problem("Entity set 'rrhhContext.empleados'  is null.");
            }
            var empleado = await _context.empleados.FindAsync(id);
            if (empleado != null)
            {
                _context.empleados.Remove(empleado);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool empleadoExists(int id)
        {
          return _context.empleados.Any(e => e.id == id);
        }

    }
}
