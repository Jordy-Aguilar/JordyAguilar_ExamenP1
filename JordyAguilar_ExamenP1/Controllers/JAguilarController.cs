using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JordyAguilar_ExamenP1.Data;
using JordyAguilar_ExamenP1.Models;
using JordyAguilar_ExamenP1.Models;

namespace JordyAguilar_ExamenP1Context.Controllers
{
    public class JAguilarController : Controller
    {
        private readonly JordyAguilar_ExamenP1Context _context;

        public JAguilarController(JordyAguilar_ExamenP1Context context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var JordyAguilar_ExamenP1Context = _context.JAguilar.Include(e => e.Celular);
            return View(await JordyAguilar_ExamenP1Context.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var JAguilar = await _context.JAguilar
                .Include(e => e.Celular)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (JAguilar == null)
            {
                return NotFound();
            }

            return View(JAguilar);
        }

        public IActionResult Create()
        {
            ViewData["IdCelular"] = new SelectList(_context.Set<Celular>(), "Id", "Id");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Sueldo,Nombre,Correo,ClienteAntiguo,Pedido,IdCelular")] JAguilar JAguilar)
        {
            if (ModelState.IsValid)
            {
                _context.Add(JAguilar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCelular"] = new SelectList(_context.Set<Celular>(), "Id", "Id", JAguilar.IdCelular);
            return View(JAguilar);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var JAguilar = await _context.JAguilar.FindAsync(id);
            if (JAguilar == null)
            {
                return NotFound();
            }
            ViewData["IdCelular"] = new SelectList(_context.Set<Celular>(), "Id", "Id", JAguilar.IdCelular);
            return View(JAguilar);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Sueldo,Nombre,Correo,ClienteAntiguo,Pedido,IdCelular")] JAguilar JAguilar)
        {
            if (id != JAguilar.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(JAguilar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JAguilarExists(JAguilar.Id))
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
            ViewData["IdCelular"] = new SelectList(_context.Set<Celular>(), "Id", "Id", JAguilar.IdCelular);
            return View(JAguilar);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var JAguilar = await _context.JAguilar
                .Include(e => e.Celular)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (JAguilar == null)
            {
                return NotFound();
            }

            return View(JAguilar);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var JAguilar = await _context.JAguilar.FindAsync(id);
            if (JAguilar != null)
            {
                _context.JAguilar.Remove(JAguilar);
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