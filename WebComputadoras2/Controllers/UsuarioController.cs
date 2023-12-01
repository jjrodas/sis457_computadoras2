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
    public class UsuarioController : Controller
    {
        private readonly FinalComputadoras2Context _context;

        public UsuarioController(FinalComputadoras2Context context)
        {
            _context = context;
        }

        // GET: Usuario
        public async Task<IActionResult> Index()
        {
            var finalComputadoras2Context = _context.Usuarios.Where(x => x.Estado != -1).Include(u => u.IdRolNavigation);
            return View(await finalComputadoras2Context.ToListAsync());
        }

        // GET: Usuario/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Usuarios == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .Include(u => u.IdRolNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: Usuario/Create
        public IActionResult Create()
        {
            ViewData["IdRol"] = new SelectList(_context.Rols, "Id", "Nombre");
            return View();
        }

        // POST: Usuario/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdRol,Nombres,Apellidos,Email,Usuario1,Clave,Telefono,FechaNacimiento")] Usuario usuario)
        {
            if (!string.IsNullOrEmpty(usuario.Nombres) && !string.IsNullOrEmpty(usuario.Apellidos) && !string.IsNullOrEmpty(usuario.Email)
                && !string.IsNullOrEmpty(usuario.Usuario1) && !string.IsNullOrEmpty(usuario.Clave))
            {
                usuario.UsuarioRegistro = "Sis-457";
                usuario.FechaRegistro = DateTime.Now;
                usuario.Estado = 1;
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdRol"] = new SelectList(_context.Rols, "Id", "Id", usuario.IdRol);
            return View(usuario);
        }

        // GET: Usuario/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Usuarios == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            ViewData["IdRol"] = new SelectList(_context.Rols, "Id", "Nombre", usuario.IdRol);
            return View(usuario);
        }

        // POST: Usuario/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdRol,Nombres,Apellidos,Email,Usuario1,Clave,Telefono,FechaNacimiento,UsuarioRegistro,FechaRegistro,Estado")] Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(usuario.Nombres) && !string.IsNullOrEmpty(usuario.Apellidos) && !string.IsNullOrEmpty(usuario.Email)
                && !string.IsNullOrEmpty(usuario.Usuario1) && !string.IsNullOrEmpty(usuario.Clave))
            {
                try
                {
                    _context.Update(usuario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.Id))
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
            ViewData["IdRol"] = new SelectList(_context.Rols, "Id", "Id", usuario.IdRol);
            return View(usuario);
        }

        // GET: Usuario/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Usuarios == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .Include(u => u.IdRolNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Usuarios == null)
            {
                return Problem("Entity set 'FinalComputadoras2Context.Usuarios'  is null.");
            }
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                usuario.Estado = -1;
                //_context.Usuarios.Remove(usuario);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {
          return (_context.Usuarios?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
