using Microsoft.EntityFrameworkCore;
using Apianimales.Models;

namespace Apianimales.Data
{

        public class ApplicationDbContext : DbContext
        {
            public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
                : base(options)
            {
            }

            public DbSet<Animal> Animales { get; set; }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                if (!optionsBuilder.IsConfigured)
                {
                    optionsBuilder.UseMySql("Server=localhost;Database=animalesdb;User=root;Password=4602;",
                        new MySqlServerVersion(new Version(10, 4, 0))); // Ajusta la versión según tu MariaDB
                }
            }
        }
    }
