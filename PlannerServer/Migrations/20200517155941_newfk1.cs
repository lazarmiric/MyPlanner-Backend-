using Microsoft.EntityFrameworkCore.Migrations;

namespace PlannerServer.Migrations
{
    public partial class newfk1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_AspNetUsers_Id",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_Id",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Tasks");

            migrationBuilder.AddColumn<string>(
                name: "UserID",
                table: "Tasks",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_UserID",
                table: "Tasks",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_AspNetUsers_UserID",
                table: "Tasks",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_AspNetUsers_UserID",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_UserID",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Tasks");

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "Tasks",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_Id",
                table: "Tasks",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_AspNetUsers_Id",
                table: "Tasks",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
