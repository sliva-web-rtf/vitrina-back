using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vitrina.Infrastructure.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddedNewFieldsToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Surname",
                table: "AspNetUsers",
                newName: "Patronymic");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Projects",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EducationLevel_Temp",
                table: "AspNetUsers",
                type: "integer",
                nullable: true);
            migrationBuilder.Sql("UPDATE AspNetUsers " +
                                 "SET EducationLevel_Temp = " +
                                 "CASE " +
                                 "WHEN EducationLevel = 'Бакалавриат' THEN 0 " +
                                 "WHEN EducationLevel = 'Специалитет' THEN 1 " +
                                 "WHEN EducationLevel = 'Магистратура' THEN 2 " +
                                 "WHEN EducationLevel = 'Аспирантура' THEN 3 " +
                                 "ELSE 4 " +
                                 "END");
            migrationBuilder.DropColumn(name: "EducationLevel", table: "AspNetUsers");
            migrationBuilder.RenameColumn(name: "EducationLevel_Temp", table: "AspNetUsers", newName: "EducationLevel");

            migrationBuilder.AddColumn<string>(
                name: "Company",
                table: "AspNetUsers",
                type: "text",
                unicode: false,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Post",
                table: "AspNetUsers",
                type: "text",
                unicode: false,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfileData",
                table: "AspNetUsers",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Resume",
                table: "AspNetUsers",
                type: "text",
                unicode: false,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Specialization",
                table: "AspNetUsers",
                type: "text",
                unicode: false,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Telegram",
                table: "AspNetUsers",
                type: "text",
                unicode: false,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_UserId",
                table: "Projects",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_AspNetUsers_UserId",
                table: "Projects",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_AspNetUsers_UserId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_UserId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Company",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Post",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ProfileData",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Resume",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Specialization",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Telegram",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "Patronymic",
                table: "AspNetUsers",
                newName: "Surname");

            migrationBuilder.AddColumn<string>(
                name: "EducationLevel_Temp",
                table: "AspNetUsers",
                type: "text",
                nullable: true);
            migrationBuilder.Sql("UPDATE AspNetUsers "+
                                 "SET EducationLevel_Temp = " +
                                 "CASE " +
                                 "WHEN EducationLevel = 0 THEN 'Бакалавриат' " +
                                 "WHEN EducationLevel = 1 THEN 'Специалитет' " +
                                 "WHEN EducationLevel = 2 THEN 'Магистратура' " +
                                 "WHEN EducationLevel = 3 THEN 'Аспирантура' " +
                                 "ELSE NULL " +
                                 "END");
            migrationBuilder.DropColumn(name: "EducationLevel", table: "AspNetUsers");
            migrationBuilder.RenameColumn(name: "EducationLevel_Temp", table: "AspNetUsers", newName: "EducationLevel");
        }
    }
}
