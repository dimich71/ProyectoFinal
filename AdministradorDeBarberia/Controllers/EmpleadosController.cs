using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AdministradorDeBarberia.Data;
using AdministradorDeBarberia.Models;

namespace AdministradorDeBarberia.Controllers
{
    public class EmpleadosController : Controller
    {
        private readonly AdministradorDeBarberiaContext _context;

        public EmpleadosController(AdministradorDeBarberiaContext context)
        {
            _context = context;
        }

        // GET: Empleados
        public async Task<IActionResult> Index()
        {
            try
            {
                return View(await _context.Empleado.ToListAsync());
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Ocurrió un error al obtener los empleados.");
                return View(new List<Empleado>());
            }
        }

        // GET: Empleados/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var empleado = await _context.Empleado
                    .FirstOrDefaultAsync(m => m.EmpleadoId == id);
                if (empleado == null)
                {
                    return NotFound();
                }

                return View(empleado);
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Ocurrió un error al obtener los detalles del empleado.");
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Empleados/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Empleados/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmpleadoId,Nombre,Especialidad")] Empleado empleado)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(empleado);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Ocurrió un error al guardar el empleado.");
                }
            }
            return View(empleado);
        }

        // GET: Empleados/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var empleado = await _context.Empleado.FindAsync(id);
                if (empleado == null)
                {
                    return NotFound();
                }
                return View(empleado);
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Ocurrió un error al obtener el empleado para editar.");
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Empleados/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmpleadoId,Nombre,Especialidad")] Empleado empleado)
        {
            if (id != empleado.EmpleadoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(empleado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmpleadoExists(empleado.EmpleadoId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Ocurrió un error al actualizar el empleado.");
                    return View(empleado);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(empleado);
        }

        // GET: Empleados/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var empleado = await _context.Empleado
                    .FirstOrDefaultAsync(m => m.EmpleadoId == id);
                if (empleado == null)
                {
                    return NotFound();
                }

                return View(empleado);
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Ocurrió un error al obtener el empleado.");
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Empleados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var empleado = await _context.Empleado.FindAsync(id);
                if (empleado != null)
                {
                    _context.Empleado.Remove(empleado);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Ocurrió un error al eliminar el empleado.");
                return RedirectToAction(nameof(Index));
            }
        }

        private bool EmpleadoExists(int id)
        {
            return _context.Empleado.Any(e => e.EmpleadoId == id);
        }
    }
}
