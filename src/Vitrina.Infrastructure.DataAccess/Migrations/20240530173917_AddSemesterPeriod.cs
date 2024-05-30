using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vitrina.Infrastructure.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddSemesterPeriod : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Period",
                table: "Projects",
                type: "text",
                unicode: false,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Semester",
                table: "Projects",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_Client",
                table: "Projects",
                column: "Client");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_Period",
                table: "Projects",
                column: "Period");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_Semester",
                table: "Projects",
                column: "Semester");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Projects_Client",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_Period",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_Semester",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Period",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Semester",
                table: "Projects");
        }
    }
}
