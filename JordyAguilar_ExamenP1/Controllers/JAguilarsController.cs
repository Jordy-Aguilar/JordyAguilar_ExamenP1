using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JordyAguilar_ExamenP1.Data;
using JordyAguilar_ExamenP1.Models;

namespace JordyAguilar_ExamenP1.Controllers
{
    public class JAguilarsController : Controller
    {
        private readonly JordyAguilar_ExamenP1Context _context;

        public JAguilarsController(JordyAguilar_ExamenP1Context context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var jordyAguilar_ExamenP1Context = _context.JAguilar.Include(j => j.Celular);
            return View(await jordyAguilar_ExamenP1Context.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jAguilar = await _context.JAguilar.Include(j => j.Celular).FirstOrDefaultAsync(m => m.Id == id);
            if (jAguilar == null)
            {
                return NotFound();
            }

            return View(jAguilar);
        }

        public IActionResult Create()
        {
            ViewData["IdCelular"] = new SelectList(_context.Set<Celulares>(), "Id", "Modelo");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Sueldo,Nombre,Correo,ClienteAntiguo,Pedido,IdCelular")] JAguilar jAguilar)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jAguilar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCelular"] = new SelectList(_context.Set<Celulares>(), "Id", "Modelo", jAguilar.IdCelular);
            return View(jAguilar);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jAguilar = await _context.JAguilar.FindAsync(id);
            if (jAguilar == null)
            {
                return NotFound();
            }
            ViewData["IdCelular"] = new SelectList(_context.Set<Celulares>(), "Id", "Modelo", jAguilar.IdCelular);
            return View(jAguilar);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Sueldo,Nombre,Correo,ClienteAntiguo,Pedido,IdCelular")] JAguilar jAguilar)
        {
            if (id != jAguilar.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jAguilar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JAguilarExists(jAguilar.Id))
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
            ViewData["IdCelular"] = new SelectList(_context.Set<Celulares>(), "Id", "Modelo", jAguilar.IdCelular);
            return View(jAguilar);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jAguilar = await _context.JAguilar.Include(j => j.Celular).FirstOrDefaultAsync(m => m.Id == id);
            if (jAguilar == null)
            {
                return NotFound();
            }

            return View(jAguilar);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jAguilar = await _context.JAguilar.FindAsync(id);
            if (jAguilar != null)
            {
                _context.JAguilar.Remove(jAguilar);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JAguilarExists(int id)
        {
            return _context.JAguilar.Any(e => e.Id == id);
        }
    }
}
