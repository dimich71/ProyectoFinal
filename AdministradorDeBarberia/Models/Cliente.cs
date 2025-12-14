using System.ComponentModel.DataAnnotations;

namespace AdministradorDeBarberia.Models
{
    public class Cliente
    {
        [Key]
        public int ClienteId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }

        [Required]
        [Phone]
        [MaxLength(20)]
        [Display(Name = "Teléfono")]
        public string Telefono { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(200)]
        [Display(Name = "Correo electrónico")]
        public string Correo { get; set; }

        // Propiedad de navegación para la relación uno a muchos
        public ICollection<Cita> ListaCitas { get; set; } = new List<Cita>();
    }
}
