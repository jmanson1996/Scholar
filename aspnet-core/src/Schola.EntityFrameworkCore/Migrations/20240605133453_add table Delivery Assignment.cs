using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Schola.Migrations
{
    /// <inheritdoc />
    public partial class addtableDeliveryAssignment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeliveryAssignment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserStudentId = table.Column<long>(type: "bigint", nullable: false),
                    AsignationId = table.Column<int>(type: "int", nullable: false),
                    isPresent = table.Column<bool>(type: "bit", nullable: false),
                    Qualification = table.Column<double>(type: "float", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<bool>(type: "bit", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryAssignment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeliveryAssignment_Asignation",
                        column: x => x.AsignationId,
                        principalTable: "Asignation",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DeliveryAssignment_User",
                        column: x => x.UserStudentId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryAssignment_AsignationId",
                table: "DeliveryAssignment",
                column: "AsignationId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryAssignment_UserStudentId",
                table: "DeliveryAssignment",
                column: "UserStudentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeliveryAssignment");
        }
    }
}
