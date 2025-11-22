// En Todo.WebApi/Repositories/IBookRepository.cs
using Todo.WebApi.Models;

namespace Todo.WebApi.Repositories
{
    public interface IBookRepository : IRepository<Book>
    {
        // Métodos específicos para buscar o filtrar libros si son necesarios
        Task<IEnumerable<Book>> GetBooksByAuthorAsync(string author);
    }
}
