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
    public class CompraDetalleController : Controller
    {
        private readonly FinalComputadoras2Context _context;

        public CompraDetalleController(FinalComputadoras2Context context)
        {
            _context = context;
        }

        // GET: CompraDetalle
        public async Task<IActionResult> Index()
        {
            var finalComputadoras2Context = _context.CompraDetalles.Include(c => c.IdCompraNavigation).Include(c => c.IdProductoNavigation);
            return View(await finalComputadoras2Context.ToListAsync());
        }

        // GET: CompraDetalle/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CompraDetalles == null)
            {
                return NotFound();
            }

            var compraDetalle = await _context.CompraDetalles
                .Include(c => c.IdCompraNavigation)
                .Include(c => c.IdProductoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (compraDetalle == null)
            {
                return NotFound();
            }

            return View(compraDetalle);
        }

        // GET: CompraDetalle/Create
        public IActionResult Create()
        {
            ViewData["IdCompra"] = new SelectList(_context.Compras, "Id", "Id");
            ViewData["IdProducto"] = new SelectList(_context.Productos, "Id", "Id");
            return View();
        }

        // POST: CompraDetalle/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdCompra,IdProducto,Cantidad,PrecioUnitario,Total,UsuarioRegistro,FechaRegistro,Estado")] CompraDetalle compraDetalle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(compraDetalle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCompra"] = new SelectList(_context.Compras, "Id", "Id", compraDetalle.IdCompra);
            ViewData["IdProducto"] = new SelectList(_context.Productos, "Id", "Id", compraDetalle.IdProducto);
            return View(compraDetalle);
        }

        // GET: CompraDetalle/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CompraDetalles == null)
            {
                return NotFound();
            }

            var compraDetalle = await _context.CompraDetalles.FindAsync(id);
            if (compraDetalle == null)
            {
                return NotFound();
            }
            ViewData["IdCompra"] = new SelectList(_context.Compras, "Id", "Id", compraDetalle.IdCompra);
            ViewData["IdProducto"] = new SelectList(_context.Productos, "Id", "Id", compraDetalle.IdProducto);
            return View(compraDetalle);
        }

        // POST: CompraDetalle/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdCompra,IdProducto,Cantidad,PrecioUnitario,Total,UsuarioRegistro,FechaRegistro,Estado")] CompraDetalle compraDetalle)
        {
            if (id != compraDetalle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(compraDetalle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompraDetalleExists(compraDetalle.Id))
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
            ViewData["IdCompra"] = new SelectList(_context.Compras, "Id", "Id", compraDetalle.IdCompra);
            ViewData["IdProducto"] = new SelectList(_context.Productos, "Id", "Id", compraDetalle.IdProducto);
            return View(compraDetalle);
        }

        // GET: CompraDetalle/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CompraDetalles == null)
            {
                return NotFound();
            }

            var compraDetalle = await _context.CompraDetalles
                .Include(c => c.IdCompraNavigation)
                .Include(c => c.IdProductoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (compraDetalle == null)
            {
                return NotFound();
            }

            return View(compraDetalle);
        }

        // POST: CompraDetalle/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CompraDetalles == null)
            {
                return Problem("Entity set 'FinalComputadoras2Context.CompraDetalles'  is null.");
            }
            var compraDetalle = await _context.CompraDetalles.FindAsync(id);
            if (compraDetalle != null)
            {
                _context.CompraDetalles.Remove(compraDetalle);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompraDetalleExists(int id)
        {
          return (_context.CompraDetalles?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
