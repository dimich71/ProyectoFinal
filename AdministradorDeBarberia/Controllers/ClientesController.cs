using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AdministradorDeBarberia.Data;
using AdministradorDeBarberia.Models;
using System.ComponentModel.DataAnnotations;

namespace AdministradorDeBarberia.Controllers
{
    public class ClientesController : Controller
    {
        private readonly AdministradorDeBarberiaContext _context;

        public ClientesController(AdministradorDeBarberiaContext context)
        {
            _context = context;
        }

        // GET: Clientes
        public async Task<IActionResult> Index()
        {
            try
            {
                return View(await _context.Cliente.ToListAsync());
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocurrió un error al obtener los clientes.");
                return View(new List<Cliente>());
            }
        }

        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var cliente = await _context.Cliente
                    .FirstOrDefaultAsync(m => m.ClienteId == id);
                if (cliente == null)
                {
                    return NotFound();
                }

                return View(cliente);
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Ocurrió un error al obtener los detalles del cliente.");
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Clientes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClienteId,Nombre,Telefono,Correo")] Cliente cliente)
        {
            // Validación lógica del correo
            var emailAttr = new EmailAddressAttribute();
            if (!emailAttr.IsValid(cliente.Correo))
            {
                ModelState.AddModelError("Correo", "El correo no tiene un formato válido.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(cliente);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    ModelState.AddModelError("", "Ocurrió un error al guardar el cliente.");
                }
            }
            return View(cliente);
        }

        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var cliente = await _context.Cliente.FindAsync(id);
                if (cliente == null)
                {
                    return NotFound();
                }
                return View(cliente);
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Ocurrió un error al obtener el cliente para editar.");
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClienteId,Nombre,Telefono,Correo")] Cliente cliente)
        {
            if (id != cliente.ClienteId)
            {
                return NotFound();
            }

            var emailAttr = new EmailAddressAttribute();
            if (!emailAttr.IsValid(cliente.Correo))
            {
                ModelState.AddModelError("Correo", "El correo no tiene un formato válido.");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cliente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(cliente.ClienteId))
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
                    ModelState.AddModelError("", "Ocurrió un error al actualizar el cliente.");
                    return View(cliente);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                var cliente = await _context.Cliente
                    .FirstOrDefaultAsync(m => m.ClienteId == id);
                if (cliente == null)
                {
                    return NotFound();
                }

                return View(cliente);
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Ocurrió un error al obtener el cliente.");
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var cliente = await _context.Cliente.FindAsync(id);
                if (cliente != null)
                {
                    _context.Cliente.Remove(cliente);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Ocurrió un error al eliminar el cliente.");
                return RedirectToAction(nameof(Index));
            }
        }

        private bool ClienteExists(int id)
        {
            return _context.Cliente.Any(e => e.ClienteId == id);
        }
    }
}
