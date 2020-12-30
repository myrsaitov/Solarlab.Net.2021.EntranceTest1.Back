using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Context.Migrations
{
    public partial class UserEmailsToAdvWereAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "eMail",
                table: "MyEvents",
                maxLength: 32,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "eMail",
                table: "MyEvents");
        }
    }
}
