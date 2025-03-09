using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vitrina.Infrastructure.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedUsersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                "UPDATE \"AspNetUsers\" " +
                "SET \"ProfileData\" = jsonb_build_object(" +
                "'email', \"Email\", " +
                "'firstName', \"FirstName\", " +
                "'lastName', \"LastName\", " +
                "'patronymic', \"Patronymic\")");

            migrationBuilder.AlterColumn<int>(
                name: "EducationLevel",
                table: "AspNetUsers",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<List<string>>(
                name: "RolesInTeam",
                table: "AspNetUsers",
                type: "text[]",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                "UPDATE \"AspNetUsers\" " +
                "SET \"ProfileData\" = '{}'::jsonb");

            migrationBuilder.DropColumn(
                name: "RolesInTeam",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "EducationLevel",
                table: "AspNetUsers",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);
        }
    }
}
