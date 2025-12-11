using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FU_House_Finder.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModelHouseAndRoom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Rooms_HouseId",
                table: "Rooms",
                column: "HouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Houses_HouseId",
                table: "Rooms",
                column: "HouseId",
                principalTable: "Houses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Houses_HouseId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_HouseId",
                table: "Rooms");
        }
    }
}
