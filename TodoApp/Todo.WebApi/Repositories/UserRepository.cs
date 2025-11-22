// En Todo.WebApi/Repositories/UserRepository.cs
using Todo.WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Todo.WebApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TODOAppDbContext _context;

        public UserRepository(TODOAppDbContext context)
        {
            _context = context;
        }

        // Implementación de IRepository<User>
        public async Task<IEnumerable<User>> GetAllAsync() => await _context.Users.ToListAsync();
        public async Task<User?> GetByIdAsync(int id) => await _context.Users.FindAsync(id);
        public async Task AddAsync(User entity) => await _context.Users.AddAsync(entity);
        public void Update(User entity) => _context.Users.Update(entity);
        public void Delete(User entity) => _context.Users.Remove(entity);
        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();

        // Implementación de IUserRepository
        public async Task<User?> GetByEmailAsync(string email) =>
            await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }
}
