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
            if (!User.Identity.IsAuthenticated)
            {
                return View();
            }

            var mailUsuarioActivo = User.Identity.Name;

            var usu = _context.Usuarios
                            .Where(e => e.Email == email)
                            .Select(e => e.Id)
                            .FirstOrDefault();

            var contexto = _context.Reservas.Include(r => r.Plaza).Include(r => r.Usuario);
            /*return View(await contexto
                .Where(c => c.Estado == "Activa" && c.UsuarioId == usu)
                .ToListAsync());*/

            return View("IndexAuth", contexto.Where(c => c.Estado == "Activa" && c.UsuarioId == usu).ToList());


           
        }
        public async Task<IActionResult> Historial()
        {
            string email = User.Identity?.Name;
            
            
            var usuario = _context.Usuarios
                            .Where(e => e.Email == email)
                            .Select(e => e.Id)
                            .FirstOrDefault();

            var contexto = _context.Reservas.Include(r => r.Plaza).Include(r => r.Usuario);
            return View(await contexto
                .Where(c => c.Estado == "Finalizado" && c.UsuarioId == usuario)
                .ToListAsync());
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
