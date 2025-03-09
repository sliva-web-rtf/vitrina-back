using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vitrina.Infrastructure.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RenamedUserClassField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RoleInTeam",
                table: "AspNetUsers",
                newName: "RoleOnPlatform");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "ProjectRoles",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Avatar",
                table: "AspNetUsers",
                type: "text",
                unicode: false,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectRoles_UserId",
                table: "ProjectRoles",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectRoles_AspNetUsers_UserId",
                table: "ProjectRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectRoles_AspNetUsers_UserId",
                table: "ProjectRoles");

            migrationBuilder.DropIndex(
                name: "IX_ProjectRoles_UserId",
                table: "ProjectRoles");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ProjectRoles");

            migrationBuilder.DropColumn(
                name: "Avatar",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "RoleOnPlatform",
                table: "AspNetUsers",
                newName: "RoleInTeam");
        }
    }
}
