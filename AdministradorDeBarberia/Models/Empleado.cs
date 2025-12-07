using System.ComponentModel.DataAnnotations;

namespace AdministradorDeBarberia.Models
{
    public class Empleado
    {
        [Key]
        public int EmpleadoId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }

        [Required]
        [MaxLength(100)]
        public string Especialidad { get; set; }

        public ICollection<Cita> ListaCitas { get; set; } = new List<Cita>();
    
    }
}
