using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParkingEx3.Models;

namespace ParkingEx3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Contexto _context;

        public HomeController(ILogger<HomeController> logger, Contexto context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            string email = User.Identity?.Name;

            if (!string.IsNullOrEmpty(email))
            {
                var usuario = await _context.Usuarios
                    .Where(u => u.Email == email)
                    .Select(u => new { u.Estado, u.Id })
                    .FirstOrDefaultAsync();

                if (usuario != null && usuario.Estado == "Pendiente")
                {
                    // Redirigir a la acción Edit para completar el perfil
                    return RedirectToAction("Edit", "Usuarios", new { id = usuario.Id });
                }
            }

            
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
