using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Context.Migrations
{
    public partial class MyEventTagsWereAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MyEventTag_MyEvents_MyEventId",
                table: "MyEventTag");

            migrationBuilder.DropForeignKey(
                name: "FK_MyEventTag_Tags_TagId",
                table: "MyEventTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MyEventTag",
                table: "MyEventTag");

            migrationBuilder.RenameTable(
                name: "MyEventTag",
                newName: "MyEventTags");

            migrationBuilder.RenameIndex(
                name: "IX_MyEventTag_TagId",
                table: "MyEventTags",
                newName: "IX_MyEventTags_TagId");

            migrationBuilder.AlterColumn<string>(
                name: "TagText",
                table: "Tags",
                maxLength: 32,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MyEventTags",
                table: "MyEventTags",
                columns: new[] { "MyEventId", "TagId" });

            migrationBuilder.AddForeignKey(
                name: "FK_MyEventTags_MyEvents_MyEventId",
                table: "MyEventTags",
                column: "MyEventId",
                principalTable: "MyEvents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MyEventTags_Tags_TagId",
                table: "MyEventTags",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MyEventTags_MyEvents_MyEventId",
                table: "MyEventTags");

            migrationBuilder.DropForeignKey(
                name: "FK_MyEventTags_Tags_TagId",
                table: "MyEventTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MyEventTags",
                table: "MyEventTags");

            migrationBuilder.RenameTable(
                name: "MyEventTags",
                newName: "MyEventTag");

            migrationBuilder.RenameIndex(
                name: "IX_MyEventTags_TagId",
                table: "MyEventTag",
                newName: "IX_MyEventTag_TagId");

            migrationBuilder.AlterColumn<string>(
                name: "TagText",
                table: "Tags",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 32,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MyEventTag",
                table: "MyEventTag",
                columns: new[] { "MyEventId", "TagId" });

            migrationBuilder.AddForeignKey(
                name: "FK_MyEventTag_MyEvents_MyEventId",
                table: "MyEventTag",
                column: "MyEventId",
                principalTable: "MyEvents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MyEventTag_Tags_TagId",
                table: "MyEventTag",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
