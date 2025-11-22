// En Todo.WebApi/Controllers/UsersController.cs
using Microsoft.AspNetCore.Mvc;
using Todo.WebApi.Models;
using Todo.WebApi.Repositories;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    // Inyección de dependencias del repositorio
    public UsersController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    // GET: api/Users
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
        // Lógica para listar usuarios (aún falta implementar DTOs y lógica de hashing)
        return Ok(await _userRepository.GetAllAsync());
    }

    // ... Continúa implementando los métodos GET by ID, POST, PUT, DELETE
}
