using Microsoft.EntityFrameworkCore.Migrations;

namespace PlannerServer.Migrations
{
    public partial class newTaskDays : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LeftDays",
                table: "Tasks",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LeftDays",
                table: "Tasks");
        }
    }
}
