using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Schola.Migrations
{
    /// <inheritdoc />
    public partial class addatributtisPresentinAssistance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isPresent",
                table: "Assistance",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isPresent",
                table: "Assistance");
        }
    }
}
