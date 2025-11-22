// En Todo.WebApi/Models/User.cs
using System.ComponentModel.DataAnnotations;

namespace Todo.WebApi.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; } // Nombre

        [Required]
        [StringLength(100)]
        public string LastName { get; set; } // Apellido

        [Required]
        [EmailAddress]
        public string Email { get; set; } // Dirección de correo electrónico

        [Required]
        public string PasswordHash { get; set; } // Contraseña (debe almacenarse hasheada por seguridad)

        // Propiedad de navegación para los libros añadidos y las reseñas
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
