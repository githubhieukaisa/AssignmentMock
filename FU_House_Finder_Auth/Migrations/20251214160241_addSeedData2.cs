using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FU_House_Finder_Auth.Migrations
{
    /// <inheritdoc />
    public partial class addSeedData2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AvatarUrl", "Email", "FullName", "PasswordHash", "PhoneNumber", "Role" },
                values: new object[] { 8, null, "landlord3@fuhouse.com", "Landlord Pending 2", "landlord3", "0123456792", 2 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 8);
        }
    }
}
