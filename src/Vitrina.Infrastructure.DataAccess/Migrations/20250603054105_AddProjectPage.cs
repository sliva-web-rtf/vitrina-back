using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Vitrina.Infrastructure.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddProjectPage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Teammates_AspNetUsers_UserId",
                table: "Teammates");

            migrationBuilder.DropForeignKey(
                name: "FK_Teammates_Projects_ProjectId",
                table: "Teammates");

            migrationBuilder.DropTable(
                name: "Block");

            migrationBuilder.DropTable(
                name: "Contents");

            migrationBuilder.DropTable(
                name: "ProjectRoleTeammate");

            migrationBuilder.DropTable(
                name: "ProjectTag");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Teammates_ProjectId",
                table: "Teammates");

            migrationBuilder.DropIndex(
                name: "IX_Projects_Client",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_Period",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_Semester",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "EmailIndex",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Teammates");

            migrationBuilder.DropColumn(
                name: "Aim",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "CompletionStatus",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Idea",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Markdown",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Period",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Problem",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Semester",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Solution",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Sphere",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "TextPreview",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "VideoUrl",
                table: "Projects");

            migrationBuilder.RenameColumn(
                name: "TypeInitiative",
                table: "Projects",
                newName: "CreatorId");

            migrationBuilder.AddColumn<Guid>(
                name: "TeamId",
                table: "Teammates",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "CuratorId",
                table: "Projects",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PageId",
                table: "Projects",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SphereId",
                table: "Projects",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TeamId",
                table: "Projects",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ThematicsId",
                table: "Projects",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TeammateId",
                table: "ProjectRoles",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfileData",
                table: "AspNetUsers",
                type: "jsonb",
                unicode: false,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "ProjectPages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ReadyStatus = table.Column<int>(type: "integer", nullable: false),
                    ProjectId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectPages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectPages_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectSpheres",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", unicode: false, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectSpheres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProjectThematics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", unicode: false, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectThematics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", unicode: false, nullable: false),
                    ProjectId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContentBlocks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PageId = table.Column<Guid>(type: "uuid", nullable: false),
                    Content = table.Column<string>(type: "jsonb", unicode: false, nullable: false),
                    ContentType = table.Column<int>(type: "integer", nullable: false),
                    NumberOnPage = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentBlocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContentBlocks_ProjectPages_PageId",
                        column: x => x.PageId,
                        principalTable: "ProjectPages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PageEditors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    PageId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PageEditors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PageEditors_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PageEditors_ProjectPages_PageId",
                        column: x => x.PageId,
                        principalTable: "ProjectPages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Teammates_TeamId",
                table: "Teammates",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_CuratorId",
                table: "Projects",
                column: "CuratorId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_PageId",
                table: "Projects",
                column: "PageId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_SphereId",
                table: "Projects",
                column: "SphereId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_TeamId",
                table: "Projects",
                column: "TeamId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ThematicsId",
                table: "Projects",
                column: "ThematicsId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectRoles_TeammateId",
                table: "ProjectRoles",
                column: "TeammateId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContentBlocks_PageId",
                table: "ContentBlocks",
                column: "PageId");

            migrationBuilder.CreateIndex(
                name: "IX_PageEditors_PageId",
                table: "PageEditors",
                column: "PageId");

            migrationBuilder.CreateIndex(
                name: "IX_PageEditors_UserId",
                table: "PageEditors",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectPages_ProjectId",
                table: "ProjectPages",
                column: "ProjectId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectSpheres_Name",
                table: "ProjectSpheres",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectThematics_Name",
                table: "ProjectThematics",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectRoles_Teammates_TeammateId",
                table: "ProjectRoles",
                column: "TeammateId",
                principalTable: "Teammates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_AspNetUsers_CuratorId",
                table: "Projects",
                column: "CuratorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_ProjectSpheres_SphereId",
                table: "Projects",
                column: "SphereId",
                principalTable: "ProjectSpheres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_ProjectThematics_ThematicsId",
                table: "Projects",
                column: "ThematicsId",
                principalTable: "ProjectThematics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Teams_TeamId",
                table: "Projects",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Teammates_AspNetUsers_UserId",
                table: "Teammates",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Teammates_Teams_TeamId",
                table: "Teammates",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectRoles_Teammates_TeammateId",
                table: "ProjectRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_AspNetUsers_CuratorId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_ProjectSpheres_SphereId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_ProjectThematics_ThematicsId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Teams_TeamId",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Teammates_AspNetUsers_UserId",
                table: "Teammates");

            migrationBuilder.DropForeignKey(
                name: "FK_Teammates_Teams_TeamId",
                table: "Teammates");

            migrationBuilder.DropTable(
                name: "ContentBlocks");

            migrationBuilder.DropTable(
                name: "PageEditors");

            migrationBuilder.DropTable(
                name: "ProjectSpheres");

            migrationBuilder.DropTable(
                name: "ProjectThematics");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "ProjectPages");

            migrationBuilder.DropIndex(
                name: "IX_Teammates_TeamId",
                table: "Teammates");

            migrationBuilder.DropIndex(
                name: "IX_Projects_CuratorId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_PageId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_SphereId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_TeamId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_ThematicsId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_ProjectRoles_TeammateId",
                table: "ProjectRoles");

            migrationBuilder.DropIndex(
                name: "EmailIndex",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "Teammates");

            migrationBuilder.DropColumn(
                name: "CuratorId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "PageId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "SphereId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ThematicsId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "TeammateId",
                table: "ProjectRoles");

            migrationBuilder.DropColumn(
                name: "ProfileData",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "CreatorId",
                table: "Projects",
                newName: "TypeInitiative");

            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "Teammates",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Aim",
                table: "Projects",
                type: "text",
                unicode: false,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CompletionStatus",
                table: "Projects",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Idea",
                table: "Projects",
                type: "text",
                unicode: false,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Markdown",
                table: "Projects",
                type: "text",
                unicode: false,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Period",
                table: "Projects",
                type: "text",
                unicode: false,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Problem",
                table: "Projects",
                type: "text",
                unicode: false,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Semester",
                table: "Projects",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Solution",
                table: "Projects",
                type: "text",
                unicode: false,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Sphere",
                table: "Projects",
                type: "text",
                unicode: false,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TextPreview",
                table: "Projects",
                type: "text",
                unicode: false,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Projects",
                type: "text",
                unicode: false,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VideoUrl",
                table: "Projects",
                type: "text",
                unicode: false,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Block",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProjectId = table.Column<int>(type: "integer", nullable: false),
                    Text = table.Column<string>(type: "text", unicode: false, nullable: false),
                    Title = table.Column<string>(type: "text", unicode: false, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Block", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Block_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProjectId = table.Column<int>(type: "integer", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", unicode: false, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contents_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectRoleTeammate",
                columns: table => new
                {
                    RolesId = table.Column<int>(type: "integer", nullable: false),
                    UsersId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectRoleTeammate", x => new { x.RolesId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_ProjectRoleTeammate_ProjectRoles_RolesId",
                        column: x => x.RolesId,
                        principalTable: "ProjectRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectRoleTeammate_Teammates_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Teammates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", unicode: false, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProjectTag",
                columns: table => new
                {
                    ProjectsId = table.Column<int>(type: "integer", nullable: false),
                    TagsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectTag", x => new { x.ProjectsId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_ProjectTag_Projects_ProjectsId",
                        column: x => x.ProjectsId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectTag_Tags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Teammates_ProjectId",
                table: "Teammates",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_Client",
                table: "Projects",
                column: "Client");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_Period",
                table: "Projects",
                column: "Period");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_Semester",
                table: "Projects",
                column: "Semester");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_Block_ProjectId",
                table: "Block",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Contents_ProjectId",
                table: "Contents",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectRoleTeammate_UsersId",
                table: "ProjectRoleTeammate",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectTag_TagsId",
                table: "ProjectTag",
                column: "TagsId");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_Name",
                table: "Tags",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Teammates_AspNetUsers_UserId",
                table: "Teammates",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Teammates_Projects_ProjectId",
                table: "Teammates",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
