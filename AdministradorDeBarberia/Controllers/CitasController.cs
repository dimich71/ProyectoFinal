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
    public class CitasController : Controller
    {
        private readonly AdministradorDeBarberiaContext _context;

        public CitasController(AdministradorDeBarberiaContext context)
        {
            _context = context;
        }

        // GET: Citas
        public async Task<IActionResult> Index()
        {
            try
            {
                var administradorDeBarberiaContext = _context.Cita.Include(c => c.Cliente).Include(c => c.Empleado).Include(c => c.Servicio);
                return View(await administradorDeBarberiaContext.ToListAsync());
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Ocurrió un error al obtener las citas.");
                return View(new List<Cita>());
            }
        }

        // GET: Citas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var cita = await _context.Cita
                    .Include(c => c.Cliente)
                    .Include(c => c.Empleado)
                    .Include(c => c.Servicio)
                    .FirstOrDefaultAsync(m => m.CitaId == id);
                if (cita == null)
                {
                    return NotFound();
                }

                return View(cita);
            }
            catch (Exception)
            {
                ModelState.AddModelError(" ", " Ocurrió un error al obtener la cita.");
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Citas/Create
        public IActionResult Create()
        {
            try
            {
                var clientes = _context.Cliente.ToList();
                var empleados = _context.Empleado.ToList();
                var servicios = _context.Servicio.ToList();

                if (!clientes.Any()) ModelState.AddModelError("", "No hay clientes registrados. Cree un cliente antes de agendar.");
                if (!empleados.Any()) ModelState.AddModelError("", "No hay empleados registrados. Cree un empleado antes de agendar.");
                if (!servicios.Any()) ModelState.AddModelError("", "No hay servicios registrados. Cree un servicio antes de agendar.");

                ViewData["ClienteId"] = new SelectList(clientes, "ClienteId", "Correo");
                ViewData["EmpleadoId"] = new SelectList(empleados, "EmpleadoId", "Especialidad");
                ViewData["ServicioId"] = new SelectList(servicios, "ServicioId", "Nombre");
                return View();
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Ocurrió un error al preparar la vista de creación de cita.");
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Citas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CitaId,FechaHora,ClienteId,EmpleadoId,ServicioId")] Cita cita)
        {
            // Validaciones lógicas
            try
            {
                if (!_context.Cliente.Any())
                {
                    ModelState.AddModelError("", "No se puede agendar una cita: no hay clientes.");
                }
                if (!_context.Empleado.Any())
                {
                    ModelState.AddModelError("", "No se puede agendar una cita: no hay empleados.");
                }
                if (!_context.Servicio.Any())
                {
                    ModelState.AddModelError("", "No se puede agendar una cita: no hay servicios.");
                }

                if (cita.FechaHora <= DateTime.Now)
                {
                    ModelState.AddModelError("FechaHora", "La fecha y hora de la cita debe ser en el futuro.");
                }

                // Verificar que los ids existen
                if (!_context.Cliente.Any(c => c.ClienteId == cita.ClienteId))
                {
                    ModelState.AddModelError("ClienteId", "Cliente seleccionado no existe.");
                }
                if (!_context.Empleado.Any(e => e.EmpleadoId == cita.EmpleadoId))
                {
                    ModelState.AddModelError("EmpleadoId", "Empleado seleccionado no existe.");
                }
                if (!_context.Servicio.Any(s => s.ServicioId == cita.ServicioId))
                {
                    ModelState.AddModelError("ServicioId", "Servicio seleccionado no existe.");
                }

                if (ModelState.IsValid)
                {
                    _context.Add(cita);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Ocurrió un error al crear la cita.");
            }

            try
            {
                ViewData["ClienteId"] = new SelectList(_context.Cliente, "ClienteId", "Correo", cita.ClienteId);
                ViewData["EmpleadoId"] = new SelectList(_context.Empleado, "EmpleadoId", "Especialidad", cita.EmpleadoId);
                ViewData["ServicioId"] = new SelectList(_context.Servicio, "ServicioId", "Nombre", cita.ServicioId);
            }
            catch (Exception)
            {
                // Si falla al cargar selectlists, agregamos error genérico
                ModelState.AddModelError("", "Ocurrió un error al preparar la lista de selección.");
            }

            return View(cita);
        }

        // GET: Citas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var cita = await _context.Cita.FindAsync(id);
                if (cita == null)
                {
                    return NotFound();
                }
                ViewData["ClienteId"] = new SelectList(_context.Cliente, "ClienteId", "Correo", cita.ClienteId);
                ViewData["EmpleadoId"] = new SelectList(_context.Empleado, "EmpleadoId", "Especialidad", cita.EmpleadoId);
                ViewData["ServicioId"] = new SelectList(_context.Servicio, "ServicioId", "Nombre", cita.ServicioId);
                return View(cita);
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Ocurrió un error al obtener la cita para editar.");
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Citas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CitaId,FechaHora,ClienteId,EmpleadoId,ServicioId")] Cita cita)
        {
            if (id != cita.CitaId)
            {
                return NotFound();
            }

            try
            {
                if (cita.FechaHora <= DateTime.Now)
                {
                    ModelState.AddModelError("FechaHora", "La fecha y hora de la cita debe ser en el futuro.");
                }

                if (!_context.Cliente.Any(c => c.ClienteId == cita.ClienteId))
                {
                    ModelState.AddModelError("ClienteId", "Cliente seleccionado no existe.");
                }
                if (!_context.Empleado.Any(e => e.EmpleadoId == cita.EmpleadoId))
                {
                    ModelState.AddModelError("EmpleadoId", "Empleado seleccionado no existe.");
                }
                if (!_context.Servicio.Any(s => s.ServicioId == cita.ServicioId))
                {
                    ModelState.AddModelError("ServicioId", "Servicio seleccionado no existe.");
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(cita);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!CitaExists(cita.CitaId))
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
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Ocurrió un error al actualizar la cita.");
            }

            ViewData["ClienteId"] = new SelectList(_context.Cliente, "ClienteId", "Correo", cita.ClienteId);
            ViewData["EmpleadoId"] = new SelectList(_context.Empleado, "EmpleadoId", "Especialidad", cita.EmpleadoId);
            ViewData["ServicioId"] = new SelectList(_context.Servicio, "ServicioId", "Nombre", cita.ServicioId);
            return View(cita);
        }

        // GET: Citas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var cita = await _context.Cita
                    .Include(c => c.Cliente)
                    .Include(c => c.Empleado)
                    .Include(c => c.Servicio)
                    .FirstOrDefaultAsync(m => m.CitaId == id);
                if (cita == null)
                {
                    return NotFound();
                }

                return View(cita);
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Ocurrió un error al obtener la cita.");
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Citas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var cita = await _context.Cita.FindAsync(id);
                if (cita != null)
                {
                    _context.Cita.Remove(cita);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Ocurrió un error al eliminar la cita.");
                return RedirectToAction(nameof(Index));
            }
        }

        private bool CitaExists(int id)
        {
            return _context.Cita.Any(e => e.CitaId == id);
        }
    }
}
