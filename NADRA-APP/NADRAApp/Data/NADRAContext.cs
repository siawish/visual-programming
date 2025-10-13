using System;
using Microsoft.EntityFrameworkCore;
using NADRAApp.Models;

namespace NADRAApp.Data
{
    public class NADRAContext : DbContext
    {
        public DbSet<Citizen> Citizens { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = "Server=localhost;Port=3306;Database=nadra_database;Uid=root;Pwd=;";
            optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 21)));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Citizen>()
                .HasIndex(c => c.CNIC)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            // Seed default admin user
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Username = "admin",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"),
                    FullName = "System Administrator",
                    Email = "admin@nadra.gov.pk",
                    Role = "Administrator",
                    CreatedDate = new DateTime(2024, 1, 1),
                    IsActive = true
                }
            );
        }
    }
}