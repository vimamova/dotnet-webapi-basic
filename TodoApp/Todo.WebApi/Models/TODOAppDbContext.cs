// En Todo.WebApi/Models/TODOAppDbContext.cs
using Microsoft.EntityFrameworkCore;

namespace Todo.WebApi.Models
{
    public class TODOAppDbContext : DbContext
    {
        public TODOAppDbContext(DbContextOptions<TODOAppDbContext> options)
            : base(options)
        {
        }

        // Nuevas colecciones requeridas:
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Review> Reviews { get; set; }

        // El DbSet<TodoItem> original debe seguir aquí si lo estás usando
        // public DbSet<TodoItem> TodoItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración de la relación entre Review, User y Book
            modelBuilder.Entity<Review>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.UserId);

            modelBuilder.Entity<Review>()
                .HasOne(r => r.Book)
                .WithMany(b => b.Reviews)
                .HasForeignKey(r => r.BookId);
        }
    }
}
