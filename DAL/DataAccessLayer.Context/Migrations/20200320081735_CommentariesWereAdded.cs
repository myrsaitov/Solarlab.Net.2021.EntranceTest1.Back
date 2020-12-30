using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Context.Migrations
{
    public partial class CommentariesWereAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Body",
                table: "MyEvents",
                maxLength: 2048,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "MyEvents",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "MyEvents",
                maxLength: 256,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Body = table.Column<string>(maxLength: 2048, nullable: true),
                    ParentMyEventId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_MyEvents_ParentMyEventId",
                        column: x => x.ParentMyEventId,
                        principalTable: "MyEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MyEvents_CategoryId",
                table: "MyEvents",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ParentMyEventId",
                table: "Comments",
                column: "ParentMyEventId");

            migrationBuilder.AddForeignKey(
                name: "FK_MyEvents_Categories_CategoryId",
                table: "MyEvents",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MyEvents_Categories_CategoryId",
                table: "MyEvents");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_MyEvents_CategoryId",
                table: "MyEvents");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "MyEvents");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "MyEvents");

            migrationBuilder.AlterColumn<string>(
                name: "Body",
                table: "MyEvents",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 2048,
                oldNullable: true);
        }
    }
}
