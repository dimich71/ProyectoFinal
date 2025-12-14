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
    public class ServiciosController : Controller
    {
        private readonly AdministradorDeBarberiaContext _context;

        public ServiciosController(AdministradorDeBarberiaContext context)
        {
            _context = context;
        }

        // GET: Servicios
        public async Task<IActionResult> Index()
        {
            try
            {
                return View(await _context.Servicio.ToListAsync());
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Ocurrió un error al obtener los servicios.");
                return View(new List<Servicio>());
            }
        }

        // GET: Servicios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var servicio = await _context.Servicio
                    .FirstOrDefaultAsync(m => m.ServicioId == id);
                if (servicio == null)
                {
                    return NotFound();
                }

                return View(servicio);
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Ocurrió un error al obtener los detalles del servicio.");
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Servicios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Servicios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ServicioId,Nombre,Precio,DuracionMinutos")] Servicio servicio)
        {
       
                try
                {
                    _context.Add(servicio);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Ocurrió un error al guardar el servicio.");
                }
          
            return View(servicio);
        }

        // GET: Servicios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var servicio = await _context.Servicio.FindAsync(id);
                if (servicio == null)
                {
                    return NotFound();
                }
                return View(servicio);
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Ocurrió un error al obtener el servicio para editar.");
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Servicios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ServicioId,Nombre,Precio,DuracionMinutos")] Servicio servicio)
        {
            if (id != servicio.ServicioId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(servicio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServicioExists(servicio.ServicioId))
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
                    ModelState.AddModelError("", "Ocurrió un error al actualizar el servicio.");
                    return View(servicio);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(servicio);
        }

        // GET: Servicios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var servicio = await _context.Servicio
                    .FirstOrDefaultAsync(m => m.ServicioId == id);
                if (servicio == null)
                {
                    return NotFound();
                }

                return View(servicio);
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Ocurrió un error al obtener el servicio.");
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Servicios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var servicio = await _context.Servicio.FindAsync(id);
                if (servicio != null)
                {
                    _context.Servicio.Remove(servicio);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Ocurrió un error al eliminar el servicio.");
                return RedirectToAction(nameof(Index));
            }
        }

        private bool ServicioExists(int id)
        {
            return _context.Servicio.Any(e => e.ServicioId == id);
        }
    }
}
