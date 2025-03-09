using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vitrina.Infrastructure.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ChangedStructureUserTeammateModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectRoles_AspNetUsers_UserId",
                table: "ProjectRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_AspNetUsers_UserId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_UserId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_ProjectRoles_UserId",
                table: "ProjectRoles");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ProjectRoles");

            migrationBuilder.DropColumn(
                name: "Specialization",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Teammates",
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

            migrationBuilder.AddColumn<int>(
                name: "RegistrationStatus",
                table: "AspNetUsers",
                type: "integer",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.Sql(
                "CREATE SEQUENCE user_id_seq START 1; " +
                "INSERT INTO \"AspNetUsers\" (\"Id\", \"Email\", \"FirstName\", \"LastName\", \"Patronymic\", \"LastTokenResetAt\", \"CreatedAt\", \"UpdatedAt\", \"RoleOnPlatform\") " +
                "SELECT DISTINCT ON (\"Email\", \"FirstName\", \"LastName\", \"Patronymic\") nextval('user_id_seq'), \"Email\", \"FirstName\", \"LastName\", \"Patronymic\", now(), now(), now(), 1 " +
                "FROM \"Teammates\";");

            migrationBuilder.Sql(
                "UPDATE \"Teammates\" " +
                "SET \"UserId\" = \"AspNetUsers\".\"Id\" " +
                "FROM \"AspNetUsers\"" +
                "WHERE \"AspNetUsers\".\"FirstName\" = \"Teammates\".\"FirstName\" AND \"AspNetUsers\".\"LastName\" = \"Teammates\".\"LastName\" AND \"AspNetUsers\".\"Patronymic\" = \"Teammates\".\"Patronymic\" AND \"AspNetUsers\".\"Email\" = \"Teammates\".\"Email\";");

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
                name: "RegistrationStatus",
                table: "AspNetUsers");

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

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Projects",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "ProjectRoles",
                type: "integer",
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

            migrationBuilder.AddColumn<string>(
                name: "Specialization",
                table: "AspNetUsers",
                type: "text",
                unicode: false,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_UserId",
                table: "Projects",
                column: "UserId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_AspNetUsers_UserId",
                table: "Projects",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
