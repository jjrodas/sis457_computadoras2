﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebComputadoras2.Models;

namespace WebComputadoras2.Controllers
{
    public class RolController : Controller
    {
        private readonly FinalComputadoras2Context _context;

        public RolController(FinalComputadoras2Context context)
        {
            _context = context;
        }

        // GET: Rol
        public async Task<IActionResult> Index()
        {
              return _context.Rols != null ? 
                          View(await _context.Rols.Where(x => x.Estado != -1).ToListAsync()) :
                          Problem("Entity set 'FinalComputadoras2Context.Rols'  is null.");
        }

        // GET: Rol/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Rols == null)
            {
                return NotFound();
            }

            var rol = await _context.Rols
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rol == null)
            {
                return NotFound();
            }

            return View(rol);
        }

        // GET: Rol/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Rol/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre")] Rol rol)
        {
            if (!string.IsNullOrEmpty(rol.Nombre))
            {
                rol.UsuarioRegistro = "Sis-457";
                rol.FechaRegistro = DateTime.Now;
                rol.Estado = 1;
                _context.Add(rol);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rol);
        }

        // GET: Rol/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Rols == null)
            {
                return NotFound();
            }

            var rol = await _context.Rols.FindAsync(id);
            if (rol == null)
            {
                return NotFound();
            }
            return View(rol);
        }

        // POST: Rol/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,UsuarioRegistro,FechaRegistro,Estado")] Rol rol)
        {
            if (id != rol.Id)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(rol.Nombre))
            {
                try
                {
                    _context.Update(rol);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RolExists(rol.Id))
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
            return View(rol);
        }

        // GET: Rol/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Rols == null)
            {
                return NotFound();
            }

            var rol = await _context.Rols
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rol == null)
            {
                return NotFound();
            }

            return View(rol);
        }

        // POST: Rol/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Rols == null)
            {
                return Problem("Entity set 'FinalComputadoras2Context.Rols'  is null.");
            }
            var rol = await _context.Rols.FindAsync(id);
            if (rol != null)
            {
                rol.Estado = -1;
                //_context.Rols.Remove(rol);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RolExists(int id)
        {
          return (_context.Rols?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
