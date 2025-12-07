using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdministradorDeBarberia.Models
{
    public class Cita
    {
        [Key]
        public int CitaId { get; set; }

        [Required]
        public DateTime FechaHora { get; set; }

        // Clave foránea para la relación uno a muchos

        [Required]
        [ForeignKey("Cliente")]
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }

        [Required]
        [ForeignKey("Empleado")]
        public int EmpleadoId { get; set; }
        public Empleado Empleado { get; set; }

        // Propiedad de navegación para la relación uno a muchos
        public ICollection<Servicio> ListaServicios { get; set; } = new List<Servicio>();
    }
}
