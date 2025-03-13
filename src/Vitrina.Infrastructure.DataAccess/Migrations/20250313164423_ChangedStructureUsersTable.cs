using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vitrina.Infrastructure.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ChangedStructureUsersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfileData",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "UserId1",
                table: "Teammates",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Telegram",
                table: "AspNetUsers",
                type: "text",
                unicode: false,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldUnicode: false,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "AspNetUsers",
                type: "text",
                unicode: false,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldUnicode: false,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "AspNetUsers",
                type: "character varying(256)",
                unicode: false,
                maxLength: 256,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldUnicode: false,
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AddColumn<List<string>>(
                name: "RolesInTeam",
                table: "AspNetUsers",
                type: "text[]",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teammates_UserId1",
                table: "Teammates",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Teammates_AspNetUsers_UserId1",
                table: "Teammates",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teammates_AspNetUsers_UserId1",
                table: "Teammates");

            migrationBuilder.DropIndex(
                name: "IX_Teammates_UserId1",
                table: "Teammates");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "Teammates");

            migrationBuilder.DropColumn(
                name: "RolesInTeam",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "Telegram",
                table: "AspNetUsers",
                type: "text",
                unicode: false,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldUnicode: false);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "AspNetUsers",
                type: "text",
                unicode: false,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldUnicode: false);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "AspNetUsers",
                type: "character varying(256)",
                unicode: false,
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldUnicode: false,
                oldMaxLength: 256);

            migrationBuilder.AddColumn<string>(
                name: "ProfileData",
                table: "AspNetUsers",
                type: "jsonb",
                nullable: false,
                defaultValue: "");
        }
    }
}
