using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vitrina.Infrastructure.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class CreateNewFieldsNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Solution",
                table: "Projects",
                type: "text",
                unicode: false,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldUnicode: false);

            migrationBuilder.AlterColumn<string>(
                name: "Problem",
                table: "Projects",
                type: "text",
                unicode: false,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldUnicode: false);

            migrationBuilder.AlterColumn<string>(
                name: "Idea",
                table: "Projects",
                type: "text",
                unicode: false,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldUnicode: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Solution",
                table: "Projects",
                type: "text",
                unicode: false,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldUnicode: false,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Problem",
                table: "Projects",
                type: "text",
                unicode: false,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldUnicode: false,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Idea",
                table: "Projects",
                type: "text",
                unicode: false,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldUnicode: false,
                oldNullable: true);
        }
    }
}
