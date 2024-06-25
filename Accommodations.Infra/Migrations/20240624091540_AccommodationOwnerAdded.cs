using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Accommodations.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AccommodationOwnerAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Accommodations",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.Sql(@"
            UPDATE Accommodations
            SET OwnerId = (SELECT Id FROM AspNetUsers WHERE Email = 'owner@test.com')
            ");

            migrationBuilder.CreateIndex(
                name: "IX_Accommodations_OwnerId",
                table: "Accommodations",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accommodations_AspNetUsers_OwnerId",
                table: "Accommodations",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accommodations_AspNetUsers_OwnerId",
                table: "Accommodations");

            migrationBuilder.DropIndex(
                name: "IX_Accommodations_OwnerId",
                table: "Accommodations");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Accommodations");
        }
    }
}
