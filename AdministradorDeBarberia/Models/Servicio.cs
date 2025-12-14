using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdministradorDeBarberia.Models
{
    public class Servicio
    {
        [Key]
        public int ServicioId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }

        [Range(0, 10000)]
        public decimal Precio { get; set; }

        [Range(1, 1440)]
        [Display(Name = "Duración (minutos)")]
        public int DuracionMinutos { get; set; }

        //Clave foranea hacia cita
        //[Required]
        //[ForeignKey("Cita")]
        //public int CitaId { get; set; }
        //public Cita Cita { get; set; } = new Cita();
    }
}
