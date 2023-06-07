using Microsoft.AspNetCore.Mvc;
using ProyectoRRHH.Models;

namespace ProyectoRRHH.Controllers
{
    public class UsuariosController : Controller
    {
        public IActionResult Registro()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Registro (RegistroViewModel model)
        {
            if (ModelState.IsValid)
            {
                return View(model);
            }
            return RedirectToAction("Index","Home");
        }
    }
}
