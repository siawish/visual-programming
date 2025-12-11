using Microsoft.EntityFrameworkCore;
using ProductMVC.Models;

namespace ProductMVC.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Electronics" },
                new Category { Id = 2, Name = "Clothing" }
            );

            
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Smartphone", Price = 75000, CategoryId = 1, InStock = true },
                new Product { Id = 2, Name = "T-Shirt", Price = 1500, CategoryId = 2, InStock = true },
                new Product { Id = 3, Name = "Laptop", Price = 150000, CategoryId = 1, InStock = true }
            );
        }
    }
}
