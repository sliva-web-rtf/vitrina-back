using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vitrina.Infrastructure.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ChangedStructureTeammatesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Avatar",
                table: "Teammates");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Teammates");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Teammates");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Teammates");

            migrationBuilder.DropColumn(
                name: "Patronymic",
                table: "Teammates");

            migrationBuilder.RenameColumn(
                name: "Surname",
                table: "AspNetUsers",
                newName: "Telegram");

            migrationBuilder.RenameColumn(
                name: "RoleInTeam",
                table: "AspNetUsers",
                newName: "RoleOnPlatform");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Teammates",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CompletionStatus",
                table: "Projects",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TextPreview",
                table: "Projects",
                type: "text",
                unicode: false,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TypeInitiative",
                table: "Projects",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<bool>(
                name: "TwoFactorEnabled",
                table: "AspNetUsers",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<bool>(
                name: "PhoneNumberConfirmed",
                table: "AspNetUsers",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean");

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

            migrationBuilder.AlterColumn<bool>(
                name: "LockoutEnabled",
                table: "AspNetUsers",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<bool>(
                name: "EmailConfirmed",
                table: "AspNetUsers",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean");

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

            migrationBuilder.AddColumn<int>(
                name: "EducationLevel_Temp",
                table: "AspNetUsers",
                type: "integer",
                nullable: true);
            migrationBuilder.Sql("UPDATE \"AspNetUsers\" " +
                                 "SET \"EducationLevel_Temp\" = " +
                                 "CASE " +
                                 "WHEN \"EducationLevel\" = 'Бакалавриат' THEN 0 " +
                                 "WHEN \"EducationLevel\" = 'Специалитет' THEN 1 " +
                                 "WHEN \"EducationLevel\" = 'Магистратура' THEN 2 " +
                                 "WHEN \"EducationLevel\" = 'Аспирантура' THEN 3 " +
                                 "ELSE 4 " +
                                 "END");
            migrationBuilder.DropColumn(name: "EducationLevel", table: "AspNetUsers");
            migrationBuilder.RenameColumn(name: "EducationLevel_Temp", table: "AspNetUsers", newName: "EducationLevel");

            migrationBuilder.AlterColumn<int>(
                name: "EducationCourse",
                table: "AspNetUsers",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "AccessFailedCount",
                table: "AspNetUsers",
                type: "integer",
                nullable: false,
                defaultValue: 5,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<string>(
                name: "Avatar",
                table: "AspNetUsers",
                type: "text",
                unicode: false,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Company",
                table: "AspNetUsers",
                type: "text",
                unicode: false,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Patronymic",
                table: "AspNetUsers",
                type: "text",
                unicode: false,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Post",
                table: "AspNetUsers",
                type: "text",
                unicode: false,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RegistrationStatus",
                table: "AspNetUsers",
                type: "integer",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<string>(
                name: "Resume",
                table: "AspNetUsers",
                type: "text",
                unicode: false,
                nullable: true);

            migrationBuilder.AddColumn<List<string>>(
                name: "RolesInTeam",
                table: "AspNetUsers",
                type: "text[]",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Specializations",
                columns: table => new
                {
                    Name = table.Column<string>(type: "text", unicode: false, nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specializations", x => x.Name);
                    table.ForeignKey(
                        name: "FK_Specializations_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Teammates_UserId",
                table: "Teammates",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Specializations_Name",
                table: "Specializations",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Specializations_UserId",
                table: "Specializations",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Teammates_AspNetUsers_UserId",
                table: "Teammates",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teammates_AspNetUsers_UserId",
                table: "Teammates");

            migrationBuilder.DropTable(
                name: "Specializations");

            migrationBuilder.DropIndex(
                name: "IX_Teammates_UserId",
                table: "Teammates");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Teammates");

            migrationBuilder.DropColumn(
                name: "CompletionStatus",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "TextPreview",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "TypeInitiative",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Avatar",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Company",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Patronymic",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Post",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RegistrationStatus",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Resume",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RolesInTeam",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "Telegram",
                table: "AspNetUsers",
                newName: "Surname");

            migrationBuilder.RenameColumn(
                name: "RoleOnPlatform",
                table: "AspNetUsers",
                newName: "RoleInTeam");

            migrationBuilder.AddColumn<byte[]>(
                name: "Avatar",
                table: "Teammates",
                type: "bytea",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Teammates",
                type: "text",
                unicode: false,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Teammates",
                type: "text",
                unicode: false,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Teammates",
                type: "text",
                unicode: false,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Patronymic",
                table: "Teammates",
                type: "text",
                unicode: false,
                nullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "TwoFactorEnabled",
                table: "AspNetUsers",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "PhoneNumberConfirmed",
                table: "AspNetUsers",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "AspNetUsers",
                type: "text",
                unicode: false,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldUnicode: false);

            migrationBuilder.AlterColumn<bool>(
                name: "LockoutEnabled",
                table: "AspNetUsers",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "EmailConfirmed",
                table: "AspNetUsers",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: false);

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
                name: "EducationLevel_Temp",
                table: "AspNetUsers",
                type: "text",
                nullable: true);
            migrationBuilder.Sql("UPDATE \"AspNetUsers\" "+
                                 "SET \"EducationLevel_Temp\" = " +
                                 "CASE " +
                                 "WHEN \"EducationLevel\" = 0 THEN 'Бакалавриат' " +
                                 "WHEN \"EducationLevel\" = 1 THEN 'Специалитет' " +
                                 "WHEN \"EducationLevel\" = 2 THEN 'Магистратура' " +
                                 "WHEN \"EducationLevel\" = 3 THEN 'Аспирантура' " +
                                 "ELSE NULL " +
                                 "END");
            migrationBuilder.DropColumn(name: "EducationLevel", table: "AspNetUsers");
            migrationBuilder.RenameColumn(name: "EducationLevel_Temp", table: "AspNetUsers", newName: "EducationLevel");

            migrationBuilder.AlterColumn<int>(
                name: "EducationCourse",
                table: "AspNetUsers",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AccessFailedCount",
                table: "AspNetUsers",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldDefaultValue: 5);
        }
    }
}
