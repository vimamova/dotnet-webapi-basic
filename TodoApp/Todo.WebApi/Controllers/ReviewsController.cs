using Microsoft.AspNetCore.Mvc;
using Todo.WebApi.Models;
using Todo.WebApi.Repositories;

[Route("api/[controller]")]
[ApiController]
public class ReviewsController : ControllerBase
{
    private readonly IReviewRepository _reviewRepository;
    private readonly IBookRepository _bookRepository;
    // En un proyecto real, también inyectarías IUserRepository para validaciones
    // private readonly IUserRepository _userRepository; 

    public ReviewsController(IReviewRepository reviewRepository, IBookRepository bookRepository)
    {
        _reviewRepository = reviewRepository;
        _bookRepository = bookRepository;
    }

    // GET: api/Reviews
    // Obtiene todas las reseñas (útil para administración o dashboard)
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Review>>> GetAllReviews()
    {
        var reviews = await _reviewRepository.GetAllAsync();
        return Ok(reviews);
    }

    // GET: api/Reviews/Book/1
    // Obtiene todas las reseñas para un libro específico.
    [HttpGet("Book/{bookId}")]
    public async Task<ActionResult<IEnumerable<Review>>> GetReviewsForBook(int bookId)
    {
        var bookExists = await _bookRepository.GetByIdAsync(bookId);
        if (bookExists == null)
        {
            return NotFound($"Libro con ID {bookId} no encontrado.");
        }

        var reviews = await _reviewRepository.GetReviewsForBookAsync(bookId);
        return Ok(reviews);
    }

    // POST: api/Reviews
    // Crea una nueva reseña/calificación.
    [HttpPost]
    public async Task<ActionResult<Review>> PostReview(Review review)
    {
        // 1. Validar que el usuario y el libro existen.
        var book = await _bookRepository.GetByIdAsync(review.BookId);
        // (La validación del usuario se haría con IUserRepository)
        if (book == null)
        {
            return BadRequest("El libro o el usuario especificado no existe.");
        }

        // 2. Verificar si el usuario ya dejó una reseña para este libro.
        var existingReview = await _reviewRepository.GetReviewByUserAndBookAsync(review.UserId, review.BookId);
        if (existingReview != null)
        {
            return Conflict("Este usuario ya ha dejado una reseña para este libro. Use PUT para actualizar.");
        }

        await _reviewRepository.AddAsync(review);
        await _reviewRepository.SaveChangesAsync();

        return CreatedAtAction(nameof(GetReviewsForBook), new { bookId = review.BookId }, review);
    }

    // PUT: api/Reviews/5
    // Actualiza una reseña existente.
    [HttpPut("{id}")]
    public async Task<IActionResult> PutReview(int id, Review review)
    {
        if (id != review.Id)
        {
            return BadRequest("El ID en la URL no coincide con el ID de la reseña.");
        }

        var existingReview = await _reviewRepository.GetByIdAsync(id);
        if (existingReview == null)
        {
            return NotFound();
        }

        // Actualizar solo los campos permitidos (Rating y TextReview)
        existingReview.Rating = review.Rating;
        existingReview.TextReview = review.TextReview;

        _reviewRepository.Update(existingReview);
        await _reviewRepository.SaveChangesAsync();

        return NoContent();
    }
}
