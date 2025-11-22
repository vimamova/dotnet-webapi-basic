using Microsoft.EntityFrameworkCore;
using Todo.WebApi.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<Todo.WebApi.Repositories.IUserRepository, Todo.WebApi.Repositories.UserRepository>();
builder.Services.AddScoped<Todo.WebApi.Repositories.IBookRepository, Todo.WebApi.Repositories.BookRepository>();
builder.Services.AddScoped<Todo.WebApi.Repositories.IReviewRepository, Todo.WebApi.Repositories.ReviewRepository>();
builder.Services.AddDbContext<TODOAppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var context = services.GetRequiredService<TODOAppDbContext>();

        if (context.Database.GetPendingMigrations().Any())
        {
            context.Database.Migrate();
        }
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Un error ocurrió al aplicar las migraciones automáticamente.");
    }
}

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseRouting();
app.UseEndpoints(Endpoint =>
{
    Endpoint.MapControllers();
}
);

app.UseHttpsRedirection();

app.Run();
