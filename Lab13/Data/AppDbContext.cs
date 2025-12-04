using Microsoft.EntityFrameworkCore;
using BooksMVC.Models;

namespace BooksMVC.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        { 
        }

        public DbSet<Book> Books { get; set; } = null!;
    }
}
