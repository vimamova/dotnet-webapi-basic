// En Todo.WebApi/Repositories/IUserRepository.cs
using Todo.WebApi.Models;

namespace Todo.WebApi.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        // Añade métodos específicos para usuarios (ej. buscar por email)
        Task<User?> GetByEmailAsync(string email);
    }
}
