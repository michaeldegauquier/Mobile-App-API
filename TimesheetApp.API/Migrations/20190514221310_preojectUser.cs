using Microsoft.EntityFrameworkCore.Migrations;

namespace TimesheetApp.API.Migrations
{
    public partial class preojectUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectUser_Projects_ProjectId",
                table: "ProjectUser");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectUser_Users_UserId",
                table: "ProjectUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectUser",
                table: "ProjectUser");

            migrationBuilder.RenameTable(
                name: "ProjectUser",
                newName: "ProjectUsers");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectUser_ProjectId",
                table: "ProjectUsers",
                newName: "IX_ProjectUsers_ProjectId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectUsers",
                table: "ProjectUsers",
                columns: new[] { "UserId", "ProjectId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectUsers_Projects_ProjectId",
                table: "ProjectUsers",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectUsers_Users_UserId",
                table: "ProjectUsers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectUsers_Projects_ProjectId",
                table: "ProjectUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectUsers_Users_UserId",
                table: "ProjectUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProjectUsers",
                table: "ProjectUsers");

            migrationBuilder.RenameTable(
                name: "ProjectUsers",
                newName: "ProjectUser");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectUsers_ProjectId",
                table: "ProjectUser",
                newName: "IX_ProjectUser_ProjectId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProjectUser",
                table: "ProjectUser",
                columns: new[] { "UserId", "ProjectId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectUser_Projects_ProjectId",
                table: "ProjectUser",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectUser_Users_UserId",
                table: "ProjectUser",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
