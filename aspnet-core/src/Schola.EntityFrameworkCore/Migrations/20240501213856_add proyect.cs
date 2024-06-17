using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Schola.Migrations
{
    /// <inheritdoc />
    public partial class addproyect : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FailReason",
                table: "AbpUserLoginAttempts",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FailReason",
                table: "AbpUserLoginAttempts");
        }
    }
}
