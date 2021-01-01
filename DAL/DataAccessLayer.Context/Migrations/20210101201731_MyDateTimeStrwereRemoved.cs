using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Context.Migrations
{
    public partial class MyDateTimeStrwereRemoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MyDateTimeStr",
                table: "MyEvents");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MyDateTimeStr",
                table: "MyEvents",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
