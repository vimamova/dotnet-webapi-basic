// En Todo.WebApi/Repositories/BookRepository.cs
using Todo.WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Todo.WebApi.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly TODOAppDbContext _context;

        public BookRepository(TODOAppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            // Incluir las reseñas para mostrar calificaciones promedio, etc.
            return await _context.Books
                .Include(b => b.Reviews)
                .ToListAsync();
        }

        public async Task<Book?> GetByIdAsync(int id)
        {
            return await _context.Books
                .Include(b => b.Reviews)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        // Operaciones CRUD genéricas
        public async Task AddAsync(Book entity) => await _context.Books.AddAsync(entity);
        public void Update(Book entity) => _context.Books.Update(entity);
        public void Delete(Book entity) => _context.Books.Remove(entity);
        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();

        // Operación específica
        public async Task<IEnumerable<Book>> GetBooksByAuthorAsync(string author)
        {
            return await _context.Books
                .Where(b => b.Author.Contains(author))
                .ToListAsync();
        }
    }
}
