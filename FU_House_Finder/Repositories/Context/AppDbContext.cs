using Microsoft.EntityFrameworkCore;
using FU_House_Finder.Repositories.Models;

namespace FU_House_Finder.Repositories.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<House> Houses { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Rate> Rates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure House entity
            modelBuilder.Entity<House>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Address).IsRequired();
                entity.Property(e => e.PowerPrice).HasPrecision(18, 2);
                entity.Property(e => e.WaterPrice).HasPrecision(18, 2);

                // Configure one-to-many relationship: House -> Rooms
                entity.HasMany(h => h.Rooms)
                    .WithOne(r => r.House)
                    .HasForeignKey(r => r.HouseId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configure Room entity
            modelBuilder.Entity<Room>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Price).HasPrecision(18, 2);
                entity.Property(e => e.HouseId).IsRequired();
            });

            // Configure Rate entity
            modelBuilder.Entity<Rate>(entity =>
            {
                entity.HasKey(e => e.Id);
            });

            // Seed House data
            modelBuilder.Entity<House>().HasData(
                new House
                {
                    Id = 1,
                    LandlordId = 1,
                    Name = "Nhà trọ Hòa Lạc A",
                    Address = "Km 29, Đại lộ Thăng Long, Hòa Lạc, Hà Nội",
                    Description = "Nhà trọ sạch sẽ, an toàn, gần trường",
                    CampusName = "Hòa Lạc",
                    PowerPrice = 3500m,
                    WaterPrice = 2000m,
                    CreatedDate = new DateTime(2024, 12, 10),
                    IsDeleted = false
                },
                new House
                {
                    Id = 2,
                    LandlordId = 2,
                    Name = "Nhà trọ Cầu Giấy B",
                    Address = "123 Phố Cầu Giấy, Cầu Giấy, Hà Nội",
                    Description = "Nhà trọ hiện đại, đủ tiện nghi",
                    CampusName = "Cầu Giấy",
                    PowerPrice = 4000m,
                    WaterPrice = 2500m,
                    CreatedDate = new DateTime(2024, 12, 10),
                    IsDeleted = false
                },
                new House
                {
                    Id = 3,
                    LandlordId = 3,
                    Name = "Nhà trọ Thanh Xuân C",
                    Address = "456 Đường Thanh Xuân, Thanh Xuân, Hà Nội",
                    Description = "Nhà trọ yên tĩnh, quanh quẩn tiện lợi",
                    CampusName = "Thanh Xuân",
                    PowerPrice = 3800m,
                    WaterPrice = 2200m,
                    CreatedDate = new DateTime(2024, 12, 10),
                    IsDeleted = false
                },
                new House
                {
                    Id = 4,
                    LandlordId = 6, // Landlord Extra
                    Name = "Landlord Extra House",
                    Address = "789 Extra St",
                    Description = "House for extra landlord",
                    CampusName = "Extra Campus",
                    PowerPrice = 3700m,
                    WaterPrice = 2100m,
                    CreatedDate = new DateTime(2025, 2, 1),
                    IsDeleted = false
                }
            );

            // Seed Room data
            modelBuilder.Entity<Room>().HasData(
                new Room
                {
                    Id = 1,
                    HouseId = 1,
                    Name = "P.101",
                    Price = 3000000m,
                    Area = 25f,
                    MaxPeople = 2,
                    Status = RoomStatus.Available,
                    Description = "Có điều hòa, nóng lạnh, WiFi"
                },
                new Room
                {
                    Id = 2,
                    HouseId = 1,
                    Name = "P.102",
                    Price = 3500000m,
                    Area = 30f,
                    MaxPeople = 3,
                    Status = RoomStatus.Available,
                    Description = "Có điều hòa, nóng lạnh, WiFi, tủ lạnh"
                },
                new Room
                {
                    Id = 3,
                    HouseId = 1,
                    Name = "P.103",
                    Price = 2800000m,
                    Area = 20f,
                    MaxPeople = 1,
                    Status = RoomStatus.Rented,
                    Description = "Có điều hòa, WiFi"
                },
                new Room
                {
                    Id = 4,
                    HouseId = 2,
                    Name = "P.201",
                    Price = 4000000m,
                    Area = 35f,
                    MaxPeople = 2,
                    Status = RoomStatus.Available,
                    Description = "Phòng cao cấp, đầy đủ tiện nghi, có bàn làm việc"
                },
                new Room
                {
                    Id = 5,
                    HouseId = 2,
                    Name = "P.202",
                    Price = 3800000m,
                    Area = 32f,
                    MaxPeople = 2,
                    Status = RoomStatus.Available,
                    Description = "Phòng rộng, có cửa sổ, điều hòa, nóng lạnh"
                },
                new Room
                {
                    Id = 6,
                    HouseId = 3,
                    Name = "P.301",
                    Price = 3200000m,
                    Area = 28f,
                    MaxPeople = 2,
                    Status = RoomStatus.Available,
                    Description = "Phòng thoáng mát, cách âm tốt, có WiFi"
                },
                new Room
                {
                    Id = 7,
                    HouseId = 4,
                    Name = "Room 3A",
                    Price = 3100000m,
                    Area = 21f,
                    MaxPeople = 2,
                    Status = RoomStatus.Available,
                    Description = "New room in extra house"
                },
                new Room
                {
                    Id = 8,
                    HouseId = 4,
                    Name = "Room 3B",
                    Price = 3300000m,
                    Area = 23f,
                    MaxPeople = 3,
                    Status = RoomStatus.Available,
                    Description = "Another room in extra house"
                }
            );
        }
    }
}
