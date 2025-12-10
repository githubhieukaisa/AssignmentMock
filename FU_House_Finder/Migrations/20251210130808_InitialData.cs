using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FU_House_Finder.Migrations
{
    /// <inheritdoc />
    public partial class InitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Houses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LandlordId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CampusName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PowerPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    WaterPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Houses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HouseId = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    Star = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HouseId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Area = table.Column<float>(type: "real", nullable: false),
                    MaxPeople = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Houses",
                columns: new[] { "Id", "Address", "CampusName", "CreatedDate", "Description", "IsDeleted", "LandlordId", "Name", "PowerPrice", "WaterPrice" },
                values: new object[,]
                {
                    { 1, "Km 29, Đại lộ Thăng Long, Hòa Lạc, Hà Nội", "Hòa Lạc", new DateTime(2024, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nhà trọ sạch sẽ, an toàn, gần trường", false, 1, "Nhà trọ Hòa Lạc A", 3500m, 2000m },
                    { 2, "123 Phố Cầu Giấy, Cầu Giấy, Hà Nội", "Cầu Giấy", new DateTime(2024, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nhà trọ hiện đại, đủ tiện nghi", false, 2, "Nhà trọ Cầu Giấy B", 4000m, 2500m },
                    { 3, "456 Đường Thanh Xuân, Thanh Xuân, Hà Nội", "Thanh Xuân", new DateTime(2024, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nhà trọ yên tĩnh, quanh quẩn tiện lợi", false, 3, "Nhà trọ Thanh Xuân C", 3800m, 2200m }
                });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "Area", "Description", "HouseId", "MaxPeople", "Name", "Price", "Status" },
                values: new object[,]
                {
                    { 1, 25f, "Có điều hòa, nóng lạnh, WiFi", 1, 2, "P.101", 3000000m, 0 },
                    { 2, 30f, "Có điều hòa, nóng lạnh, WiFi, tủ lạnh", 1, 3, "P.102", 3500000m, 0 },
                    { 3, 20f, "Có điều hòa, WiFi", 1, 1, "P.103", 2800000m, 1 },
                    { 4, 35f, "Phòng cao cấp, đầy đủ tiện nghi, có bàn làm việc", 2, 2, "P.201", 4000000m, 0 },
                    { 5, 32f, "Phòng rộng, có cửa sổ, điều hòa, nóng lạnh", 2, 2, "P.202", 3800000m, 0 },
                    { 6, 28f, "Phòng thoáng mát, cách âm tốt, có WiFi", 3, 2, "P.301", 3200000m, 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Houses");

            migrationBuilder.DropTable(
                name: "Rates");

            migrationBuilder.DropTable(
                name: "Rooms");
        }
    }
}
