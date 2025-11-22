// En Todo.WebApi/Repositories/ReviewRepository.cs
using Todo.WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Todo.WebApi.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly TODOAppDbContext _context;

        public ReviewRepository(TODOAppDbContext context)
        {
            _context = context;
        }

        // Sobreescribir el GetAll para incluir referencias
        public async Task<IEnumerable<Review>> GetAllAsync()
        {
            return await _context.Reviews
                .Include(r => r.User) // Incluye el usuario que hizo la reseña
                .Include(r => r.Book) // Incluye el libro reseñado
                .ToListAsync();
        }

        public async Task<Review?> GetByIdAsync(int id)
        {
            return await _context.Reviews
                .Include(r => r.User)
                .Include(r => r.Book)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        // Operaciones CRUD genéricas
        public async Task AddAsync(Review entity) => await _context.Reviews.AddAsync(entity);
        public void Update(Review entity) => _context.Reviews.Update(entity);
        public void Delete(Review entity) => _context.Reviews.Remove(entity);
        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();

        // Operaciones específicas
        public async Task<IEnumerable<Review>> GetReviewsForBookAsync(int bookId)
        {
            return await _context.Reviews
                .Where(r => r.BookId == bookId)
                .Include(r => r.User)
                .ToListAsync();
        }

        public async Task<Review?> GetReviewByUserAndBookAsync(int userId, int bookId)
        {
            return await _context.Reviews
                .FirstOrDefaultAsync(r => r.UserId == userId && r.BookId == bookId);
        }
    }
}
