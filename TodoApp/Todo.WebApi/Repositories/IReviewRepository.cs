// En Todo.WebApi/Repositories/IReviewRepository.cs
using Todo.WebApi.Models;

namespace Todo.WebApi.Repositories
{
    public interface IReviewRepository : IRepository<Review>
    {
        // Obtener todas las reseñas para un libro específico
        Task<IEnumerable<Review>> GetReviewsForBookAsync(int bookId);

        // Obtener la reseña de un usuario para un libro específico
        Task<Review?> GetReviewByUserAndBookAsync(int userId, int bookId);
    }
}
