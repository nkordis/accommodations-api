using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Accommodations.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AddIsAvailableToUnit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "Units",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "Units");
        }
    }
}
