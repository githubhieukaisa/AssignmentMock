using Microsoft.EntityFrameworkCore;
using FU_House_Finder_Auth.Repositories.Models;

namespace FU_House_Finder_Auth.Repositories.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Users table
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.FullName).IsRequired();
                entity.Property(e => e.Email).IsRequired();
                entity.Property(e => e.PasswordHash).IsRequired();
                entity.Property(e => e.PhoneNumber).IsRequired();
                entity.Property(e => e.Role).IsRequired();
                entity.Property(e => e.AvatarUrl).IsRequired(false);
                entity.Property(e => e.IsActive).HasDefaultValue(true);
            });

            // Seed data
            SeedUsers(modelBuilder);
        }

        private void SeedUsers(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    FullName = "Admin User",
                    Email = "admin@fuhouse.com",
                    PasswordHash = "123",
                    PhoneNumber = "0123456789",
                    Role = UserRole.Admin,
                    AvatarUrl = null,
                    IsActive = true
                },
                new User
                {
                    Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    FullName = "Staff User",
                    Email = "staff@fuhouse.com",
                    PasswordHash = "staff123",
                    PhoneNumber = "0123456790",
                    Role = UserRole.Staff,
                    AvatarUrl = null,
                    IsActive = true
                },
                new User
                {
                    Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    FullName = "Landlord User",
                    Email = "landlord@fuhouse.com",
                    PasswordHash = "landlord123",
                    PhoneNumber = "0123456791",
                    Role = UserRole.Landlord,
                    AvatarUrl = null,
                    IsActive = true
                },
                new User
                {
                    Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                    FullName = "Student User",
                    Email = "student@fuhouse.com",
                    PasswordHash = "student123",
                    PhoneNumber = "0123456792",
                    Role = UserRole.Student,
                    AvatarUrl = null,
                    IsActive = true
                }
            );
        }
    }
}
