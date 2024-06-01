using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vitrina.Infrastructure.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ChangeContent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageBytes",
                table: "Contents");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Contents",
                type: "longtext",
                unicode: false,
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Contents");

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageBytes",
                table: "Contents",
                type: "longblob",
                nullable: false);
        }
    }
}
