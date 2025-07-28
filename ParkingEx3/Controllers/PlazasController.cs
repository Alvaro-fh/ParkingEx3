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
    public class PlazasController : Controller
    {
        private readonly Contexto _context;

        public PlazasController(Contexto context)
        {
            _context = context;
        }

        // GET: Plazas
        public async Task<IActionResult> Index()
        {
            return View(await _context.Plazas.ToListAsync());
        }

        // GET: Plazas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plazas = await _context.Plazas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (plazas == null)
            {
                return NotFound();
            }

            return View(plazas);
        }

        // GET: Plazas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Plazas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Estado")] Plazas plazas)
        {
            
                _context.Add(plazas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
            return View(plazas);
        }

        // GET: Plazas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plazas = await _context.Plazas.FindAsync(id);
            if (plazas == null)
            {
                return NotFound();
            }
            return View(plazas);
        }

        // POST: Plazas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Estado")] Plazas plazas)
        {
            if (id != plazas.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(plazas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlazasExists(plazas.Id))
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
            return View(plazas);
        }

        // GET: Plazas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plazas = await _context.Plazas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (plazas == null)
            {
                return NotFound();
            }

            return View(plazas);
        }

        // POST: Plazas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var plazas = await _context.Plazas.FindAsync(id);
            if (plazas != null)
            {
                _context.Plazas.Remove(plazas);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlazasExists(int id)
        {
            return _context.Plazas.Any(e => e.Id == id);
        }
    }
}
