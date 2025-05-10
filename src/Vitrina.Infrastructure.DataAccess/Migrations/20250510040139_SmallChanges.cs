using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vitrina.Infrastructure.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SmallChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProfileData",
                table: "AspNetUsers",
                newName: "AdditionalInformation");

            migrationBuilder.AlterColumn<int>(
                name: "RegistrationStatus",
                table: "AspNetUsers",
                type: "integer",
                nullable: true,
                defaultValue: 1,
                oldClrType: typeof(int),
                oldType: "integer",
                oldDefaultValue: 1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AdditionalInformation",
                table: "AspNetUsers",
                newName: "ProfileData");

            migrationBuilder.AlterColumn<int>(
                name: "RegistrationStatus",
                table: "AspNetUsers",
                type: "integer",
                nullable: false,
                defaultValue: 1,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true,
                oldDefaultValue: 1);
        }
    }
}
