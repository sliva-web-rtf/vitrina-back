using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vitrina.Infrastructure.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddedModeration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdminViews",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AdministratorId = table.Column<int>(type: "integer", nullable: false),
                    PageId = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminViews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdminViews_AspNetUsers_AdministratorId",
                        column: x => x.AdministratorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdminViews_ProjectPages_PageId",
                        column: x => x.PageId,
                        principalTable: "ProjectPages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VerificationResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PageId = table.Column<Guid>(type: "uuid", nullable: false),
                    NewPageStatus = table.Column<int>(type: "integer", nullable: false),
                    Message = table.Column<string>(type: "text", unicode: false, nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModeratorId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VerificationResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VerificationResults_ProjectPages_PageId",
                        column: x => x.PageId,
                        principalTable: "ProjectPages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdminViews_AdministratorId",
                table: "AdminViews",
                column: "AdministratorId");

            migrationBuilder.CreateIndex(
                name: "IX_AdminViews_PageId",
                table: "AdminViews",
                column: "PageId");

            migrationBuilder.CreateIndex(
                name: "IX_VerificationResults_PageId",
                table: "VerificationResults",
                column: "PageId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminViews");

            migrationBuilder.DropTable(
                name: "VerificationResults");
        }
    }
}
