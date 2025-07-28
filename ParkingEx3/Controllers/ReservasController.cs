using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ParkingEx3.Models;

namespace ParkingEx3.Controllers
{
    public class ReservasController : Controller
    {
        private readonly Contexto _context;

        public ReservasController(Contexto context)
        {
            _context = context;
        }

        // GET: Reservas
        public async Task<IActionResult> Index()
        {
            var contexto = _context.Reservas.Include(r => r.Plaza).Include(r => r.Usuario);
            return View(await contexto
                .Where(c => c.Estado == "Activa")
                .ToListAsync());
        }

        // GET: Reservas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservas = await _context.Reservas
                .Include(r => r.Plaza)
                .Include(r => r.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservas == null)
            {
                return NotFound();
            }

            return View(reservas);
        }

        // GET: Reservas/Create
        public async Task<IActionResult> Create()
        {
            string email = User.Identity?.Name;
            
            if (!string.IsNullOrEmpty(email))
            {
                var usuario = _context.Usuarios
               .Where(e => e.Email == email)
               .Select(e => e.Id)
               .FirstOrDefault();
                int idusu = usuario;
                ViewBag.usu = idusu;
            }
            ViewData["PlazaId"] = new SelectList(_context.Plazas, "Id", "Id");
            
            //ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id");
            return View();
        }

        // POST: Reservas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UsuarioId,PlazaId,FechaInicio,FechaFin,Horas,Precio,PrecioFinal,Estado")] Reservas reservas)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reservas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PlazaId"] = new SelectList(_context.Plazas, "Id", "Id", reservas.PlazaId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", reservas.UsuarioId);
            return View(reservas);
        }

        // GET: Reservas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservas = await _context.Reservas.FindAsync(id);
            if (reservas == null)
            {
                return NotFound();
            }
            ViewData["PlazaId"] = new SelectList(_context.Plazas, "Id", "Id", reservas.PlazaId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", reservas.UsuarioId);
            return View(reservas);
        }

        // POST: Reservas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UsuarioId,PlazaId,FechaInicio,FechaFin,Horas,Precio,PrecioFinal,Estado")] Reservas reservas)
        {
            if (id != reservas.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    
                    _context.Update(reservas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservasExists(reservas.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), "Home");
            }
            ViewData["PlazaId"] = new SelectList(_context.Plazas, "Id", "Id", reservas.PlazaId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuarios, "Id", "Id", reservas.UsuarioId);
            return View(reservas);
        }

        // GET: Reservas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservas = await _context.Reservas
                .Include(r => r.Plaza)
                .Include(r => r.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservas == null)
            {
                return NotFound();
            }

            return View(reservas);
        }

        // POST: Reservas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reserva = await _context.Reservas.FindAsync(id);
            if (reserva != null)
            {
                reserva.Estado = "Finalizado";
                reserva.FechaFin = DateTime.Now;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index), "Home");
        }

        private bool ReservasExists(int id)
        {
            return _context.Reservas.Any(e => e.Id == id);
        }
    }
}
