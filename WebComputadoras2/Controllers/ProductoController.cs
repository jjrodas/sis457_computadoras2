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
    public class ProductoController : Controller
    {
        private readonly FinalComputadoras2Context _context;

        public ProductoController(FinalComputadoras2Context context)
        {
            _context = context;
        }

        // GET: Producto
        public async Task<IActionResult> Index()
        {
            var finalComputadoras2Context = _context.Productos.Where(x => x.Estado != -1).Include(p => p.IdCategoriaNavigation).Include(p => p.IdMarcaNavigation);
            return View(await finalComputadoras2Context.ToListAsync());
        }

        // GET: Producto/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Productos == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .Include(p => p.IdCategoriaNavigation)
                .Include(p => p.IdMarcaNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // GET: Producto/Create
        public IActionResult Create()
        {
            ViewData["IdCategoria"] = new SelectList(_context.Categoria, "Id", "Nombre");
            ViewData["IdMarca"] = new SelectList(_context.Marcas, "Id", "Nombre");
            return View();
        }

        // POST: Producto/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdCategoria,IdMarca,Descripcion,UrlImagen,PrecioVenta,Stock")] Producto producto)
        {
            if (!string.IsNullOrEmpty(producto.Descripcion) && !string.IsNullOrEmpty(producto.UrlImagen) && !decimal.IsNegative(producto.PrecioVenta)
                && !int.IsNegative(producto.Stock))
            {
                producto.UsuarioRegistro = "Sis-457";
                producto.FechaRegistro = DateTime.Now;
                producto.Estado = 1;
                _context.Add(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCategoria"] = new SelectList(_context.Categoria, "Id", "Id", producto.IdCategoria);
            ViewData["IdMarca"] = new SelectList(_context.Marcas, "Id", "Id", producto.IdMarca);
            return View(producto);
        }

        // GET: Producto/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Productos == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            ViewData["IdCategoria"] = new SelectList(_context.Categoria, "Id", "Nombre", producto.IdCategoria);
            ViewData["IdMarca"] = new SelectList(_context.Marcas, "Id", "Nombre", producto.IdMarca);
            return View(producto);
        }

        // POST: Producto/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdCategoria,IdMarca,Descripcion,UrlImagen,PrecioVenta,Stock,UsuarioRegistro,FechaRegistro,Estado")] Producto producto)
        {
            if (id != producto.Id)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(producto.Descripcion) && !string.IsNullOrEmpty(producto.UrlImagen) && !decimal.IsNegative(producto.PrecioVenta)
                && !int.IsNegative(producto.Stock))
            {
                try
                {
                    _context.Update(producto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoExists(producto.Id))
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
            ViewData["IdCategoria"] = new SelectList(_context.Categoria, "Id", "Id", producto.IdCategoria);
            ViewData["IdMarca"] = new SelectList(_context.Marcas, "Id", "Id", producto.IdMarca);
            return View(producto);
        }

        // GET: Producto/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Productos == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .Include(p => p.IdCategoriaNavigation)
                .Include(p => p.IdMarcaNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // POST: Producto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Productos == null)
            {
                return Problem("Entity set 'FinalComputadoras2Context.Productos'  is null.");
            }
            var producto = await _context.Productos.FindAsync(id);
            if (producto != null)
            {
                producto.Estado = -1;
                //_context.Productos.Remove(producto);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductoExists(int id)
        {
          return (_context.Productos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
