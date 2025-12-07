using System.ComponentModel.DataAnnotations;

namespace AdministradorDeBarberia.Models
{
    public enum RolUsuario
    {
        Administrador,
        Usuario
    }
    public class Usuario
    {
        [Key]
        public int UsuarioId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(200)]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public RolUsuario Rol { get; set; }
    }
}
