using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Context.Migrations
{
    public partial class @new : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_MyEvents_ParentMyEventId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_ParentMyEventId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "ParentMyEventId",
                table: "Comments");

            migrationBuilder.AddColumn<int>(
                name: "MyEventId",
                table: "Comments",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_MyEventId",
                table: "Comments",
                column: "MyEventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_MyEvents_MyEventId",
                table: "Comments",
                column: "MyEventId",
                principalTable: "MyEvents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_MyEvents_MyEventId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_MyEventId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "MyEventId",
                table: "Comments");

            migrationBuilder.AddColumn<int>(
                name: "ParentMyEventId",
                table: "Comments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ParentMyEventId",
                table: "Comments",
                column: "ParentMyEventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_MyEvents_ParentMyEventId",
                table: "Comments",
                column: "ParentMyEventId",
                principalTable: "MyEvents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
