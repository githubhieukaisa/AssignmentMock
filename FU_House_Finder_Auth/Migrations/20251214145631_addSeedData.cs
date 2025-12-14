using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FU_House_Finder_Auth.Migrations
{
    /// <inheritdoc />
    public partial class addSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AvatarUrl", "Email", "FullName", "PasswordHash", "PhoneNumber", "Role" },
                values: new object[] { 5, null, "landlord2@fuhouse.com", "Landlord Pending 1", "landlord2", "0123456791", 2 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AvatarUrl", "Email", "FullName", "IsActive", "PasswordHash", "PhoneNumber", "Role" },
                values: new object[,]
                {
                    { 6, null, "landlord3@fuhouse.com", "Landlord Extra", true, "landlord3", "0123456794", 2 },
                    { 7, null, "student3@fuhouse.com", "Student Gamma", true, "student3", "0123456795", 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 7);
        }
    }
}
