using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebComputadoras2.Models;

namespace WebComputadoras2.Controllers
{
    public class MarcaController : Controller
    {
        private readonly FinalComputadoras2Context _context;

        public MarcaController(FinalComputadoras2Context context)
        {
            _context = context;
        }

        // GET: Marca
        public async Task<IActionResult> Index()
        {
              return _context.Marcas != null ? 
                          View(await _context.Marcas.Where(x => x.Estado != -1).ToListAsync()) :
                          Problem("Entity set 'FinalComputadoras2Context.Marcas'  is null.");
        }

        // GET: Marca/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Marcas == null)
            {
                return NotFound();
            }

            var marca = await _context.Marcas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (marca == null)
            {
                return NotFound();
            }

            return View(marca);
        }

        // GET: Marca/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Marca/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre")] Marca marca)
        {
            if (!string.IsNullOrEmpty(marca.Nombre))
            {
                marca.UsuarioRegistro = "Sis-457";
                marca.FechaRegistro = DateTime.Now;
                marca.Estado = 1;
                _context.Add(marca);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(marca);
        }

        // GET: Marca/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Marcas == null)
            {
                return NotFound();
            }

            var marca = await _context.Marcas.FindAsync(id);
            if (marca == null)
            {
                return NotFound();
            }
            return View(marca);
        }

        // POST: Marca/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,UsuarioRegistro,FechaRegistro,Estado")] Marca marca)
        {
            if (id != marca.Id)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(marca.Nombre))
            {
                try
                {
                    _context.Update(marca);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MarcaExists(marca.Id))
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
            return View(marca);
        }

        // GET: Marca/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Marcas == null)
            {
                return NotFound();
            }

            var marca = await _context.Marcas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (marca == null)
            {
                return NotFound();
            }

            return View(marca);
        }

        // POST: Marca/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Marcas == null)
            {
                return Problem("Entity set 'FinalComputadoras2Context.Marcas'  is null.");
            }
            var marca = await _context.Marcas.FindAsync(id);
            if (marca != null)
            {
                marca.Estado = -1;
                //_context.Marcas.Remove(marca);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MarcaExists(int id)
        {
          return (_context.Marcas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
