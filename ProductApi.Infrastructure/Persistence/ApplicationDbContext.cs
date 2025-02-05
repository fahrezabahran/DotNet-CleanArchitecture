using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProductApi.Domain.Entities;

namespace ProductApi.Infrastructure.Persistence
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Mengatur panjang maksimum untuk kolom Name
            modelBuilder.Entity<Product>()
                .Property(p => p.Name)
                .HasMaxLength(100); // Ganti 100 dengan panjang maksimum yang diinginkan

            // Menentukan tipe kolom untuk Price
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18, 2)"); // Menentukan tipe kolom decimal dengan presisi 18 dan skala 2
        }
    }
}
