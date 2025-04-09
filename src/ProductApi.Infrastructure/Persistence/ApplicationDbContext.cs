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
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<TimeSheet> TimeSheets { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserActivity> UserActivities { get; set; }
        public DbSet<UserRole> UserRoles{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Mengatur panjang maksimum untuk kolom Name
            modelBuilder.Entity<Product>().Property(p => p.Name).HasMaxLength(100); // Ganti 100 dengan panjang maksimum yang diinginkan

            // Menentukan tipe kolom untuk Price
            modelBuilder.Entity<Product>().Property(p => p.Price).HasColumnType("decimal(18, 2)"); // Menentukan tipe kolom decimal dengan presisi 18 dan skala 2

            // Konfigurasi UserRole
            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.ToTable("UserRole");

                entity.HasKey(e => e.RoleId);

                entity.Property(e => e.RoleId)
                      .ValueGeneratedOnAdd();

                entity.Property(e => e.RoleName)
                      .IsRequired()
                      .HasMaxLength(200);

                entity.Property(e => e.IsActive)
                      .IsRequired();
            });

            // Konfigurasi User
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.HasKey(e => e.UserId);

                entity.Property(e => e.UserId)
                      .ValueGeneratedOnAdd();

                entity.Property(e => e.UserName)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(e => e.Password)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(e => e.UserRole)
                      .IsRequired();

                entity.Property(e => e.IsLogin)
                      .IsRequired();

                entity.Property(e => e.FalsePwdCount)
                      .IsRequired();

                entity.Property(e => e.IsRevoke)
                      .IsRequired();

                entity.Property(e => e.IsActive)
                      .IsRequired();
            });

            // Konfigurasi UserActivityMonitor
            modelBuilder.Entity<UserActivity>(entity =>
            {
                entity.ToTable("UserActivity");

                entity.HasKey(e => e.ActivityId);

                entity.Property(e => e.ActivityId)
                      .ValueGeneratedOnAdd();

                entity.Property(e => e.UserId)
                      .IsRequired();

                entity.Property(e => e.Login)
                      .IsRequired();

                entity.Property(e => e.Logout)
                      .IsRequired();

                entity.Property(e => e.ChangePassword)
                      .IsRequired();
            });

            modelBuilder.Entity<TimeSheet>(entity =>
            {
                // Primary Key
                entity.HasKey(e => e.Id);

                // UserId sebagai Foreign Key (Jika ada relasi dengan tabel User)
                entity.Property(e => e.UserId)
                      .IsRequired();

                // DateOnly (disimpan sebagai Date di DB)
                entity.Property(e => e.CheckInDate)
                      .HasColumnType("date")
                      .IsRequired();

                // TimeSpan (disimpan sebagai Time di DB)
                entity.Property(e => e.CheckInTime)
                      .HasColumnType("time")
                      .IsRequired();

                entity.Property(e => e.CheckOutTime)
                      .HasColumnType("time")
                      .IsRequired();

                entity.Property(e => e.TotalTime)
                      .HasColumnType("time")
                      .HasComputedColumnSql(@"
                          CONVERT(TIME, DATEADD(MINUTE, DATEDIFF(MINUTE, CheckInTime, CheckOutTime), '00:00'))
                      ");

                entity.Property(e => e.OverTime)
                      .HasColumnType("time")
                      .HasComputedColumnSql(@"
                            CASE 
                                WHEN DATEDIFF(HOUR, CheckInTime, CheckOutTime) > 9 THEN DATEADD(SECOND, DATEDIFF(SECOND, DATEADD(HOUR, 9, CheckInTime), CheckOutTime), '00:00:00') 
                                ELSE '00:00:00' 
                            END
                      ");

                // Optional fields
                entity.Property(e => e.JobDetails)
                      .HasMaxLength(500);

                entity.Property(e => e.Remarks)
                      .HasMaxLength(500);

                // Timestamps
                entity.Property(e => e.CreatedAt)
                      .HasDefaultValueSql("GETDATE()");

                entity.Property(e => e.UpdatedAt)
                      .ValueGeneratedOnUpdate();
            });
        }
    }
}
