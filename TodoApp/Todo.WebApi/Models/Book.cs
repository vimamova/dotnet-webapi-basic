// En Todo.WebApi/Models/Book.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Todo.WebApi.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        [StringLength(100)]
        public string Author { get; set; }

        public int PublicationYear { get; set; }

        public string? CoverImageUrl { get; set; } // Opcional, como se requiere

        // Propiedad de navegación para las reseñas
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
