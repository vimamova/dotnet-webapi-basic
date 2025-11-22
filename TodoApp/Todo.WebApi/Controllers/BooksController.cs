using Microsoft.AspNetCore.Mvc;
using Todo.WebApi.Models;
using Todo.WebApi.Repositories;

// NOTA: Para un proyecto real, necesitarías DTOs (Data Transfer Objects) 
// para las peticiones POST y PUT, y para evitar exponer la entidad de base de datos directamente.

[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly IBookRepository _bookRepository;

    public BooksController(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    // GET: api/Books
    // Obtiene todos los libros.
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
    {
        var books = await _bookRepository.GetAllAsync();
        return Ok(books);
    }

    // GET: api/Books/5
    // Obtiene un libro por su ID.
    [HttpGet("{id}")]
    public async Task<ActionResult<Book>> GetBook(int id)
    {
        var book = await _bookRepository.GetByIdAsync(id);

        if (book == null)
        {
            return NotFound();
        }

        return Ok(book);
    }

    // POST: api/Books
    // Crea un nuevo libro.
    [HttpPost]
    public async Task<ActionResult<Book>> PostBook(Book book)
    {
        // NOTA: Aquí podrías añadir validación adicional o lógica de negocio.
        await _bookRepository.AddAsync(book);
        await _bookRepository.SaveChangesAsync();

        return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
    }

    // PUT: api/Books/5
    // Actualiza un libro existente.
    [HttpPut("{id}")]
    public async Task<IActionResult> PutBook(int id, Book book)
    {
        if (id != book.Id)
        {
            return BadRequest();
        }

        var existingBook = await _bookRepository.GetByIdAsync(id);
        if (existingBook == null)
        {
            return NotFound();
        }

        // Actualizar las propiedades del libro existente (o usar un DTO y mapear)
        _bookRepository.Update(book);
        await _bookRepository.SaveChangesAsync();

        return NoContent();
    }

    // DELETE: api/Books/5
    // Elimina un libro.
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        var book = await _bookRepository.GetByIdAsync(id);
        if (book == null)
        {
            return NotFound();
        }

        _bookRepository.Delete(book);
        await _bookRepository.SaveChangesAsync();

        return NoContent();
    }
}
