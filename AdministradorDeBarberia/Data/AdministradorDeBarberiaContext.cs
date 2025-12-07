using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AdministradorDeBarberia.Models;

namespace AdministradorDeBarberia.Data
{
    public class AdministradorDeBarberiaContext : DbContext
    {
        public AdministradorDeBarberiaContext (DbContextOptions<AdministradorDeBarberiaContext> options)
            : base(options)
        {
        }

        public DbSet<AdministradorDeBarberia.Models.Cliente> Cliente { get; set; } = default!;
        public DbSet<AdministradorDeBarberia.Models.Cita> Cita { get; set; } = default!;
        public DbSet<AdministradorDeBarberia.Models.Servicio> Servicio { get; set; } = default!;
        public DbSet<AdministradorDeBarberia.Models.Empleado> Empleado { get; set; } = default!;
        public DbSet<AdministradorDeBarberia.Models.Usuario> Usuario { get; set; } = default!;
    }
}
