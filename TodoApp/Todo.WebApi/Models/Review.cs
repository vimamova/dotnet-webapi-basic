// En Todo.WebApi/Models/Review.cs
using System.ComponentModel.DataAnnotations;

namespace Todo.WebApi.Models
{
    public class Review
    {
        public int Id { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; } // Calificación de 1 a 5 estrellas

        [StringLength(500)]
        public string? TextReview { get; set; } // Reseña escrita (opcional)

        // Claves foráneas
        public int UserId { get; set; }
        public int BookId { get; set; }

        // Propiedades de navegación
        public User User { get; set; }
        public Book Book { get; set; }
    }
}
