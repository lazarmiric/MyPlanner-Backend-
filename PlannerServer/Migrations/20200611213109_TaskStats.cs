using Microsoft.EntityFrameworkCore.Migrations;

namespace PlannerServer.Migrations
{
    public partial class TaskStats : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Stats",
                table: "Tasks",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Stats",
                table: "Tasks");
        }
    }
}
