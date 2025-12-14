using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FU_House_Finder.Migrations
{
    /// <inheritdoc />
    public partial class addTestData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Houses",
                columns: new[] { "Id", "Address", "CampusName", "CreatedDate", "Description", "IsDeleted", "LandlordId", "Name", "PowerPrice", "WaterPrice" },
                values: new object[] { 4, "789 Extra St", "Extra Campus", new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "House for extra landlord", false, 6, "Landlord Extra House", 3700m, 2100m });

            migrationBuilder.InsertData(
                table: "Rates",
                columns: new[] { "Id", "Comment", "CreatedDate", "HouseId", "Star", "StudentId" },
                values: new object[,]
                {
                    { 2000, "Brand new house, very clean!", new DateTime(2025, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 5, 7 },
                    { 2001, "Visited as a guest, nice place.", new DateTime(2025, 2, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 4, 7 }
                });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "Area", "Description", "HouseId", "MaxPeople", "Name", "Price", "Status" },
                values: new object[,]
                {
                    { 7, 21f, "New room in extra house", 4, 2, "Room 3A", 3100000m, 0 },
                    { 8, 23f, "Another room in extra house", 4, 3, "Room 3B", 3300000m, 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Rates",
                keyColumn: "Id",
                keyValue: 2000);

            migrationBuilder.DeleteData(
                table: "Rates",
                keyColumn: "Id",
                keyValue: 2001);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
