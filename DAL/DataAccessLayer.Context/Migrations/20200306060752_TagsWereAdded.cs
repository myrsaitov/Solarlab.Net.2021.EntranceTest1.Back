using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Context.Migrations
{
    public partial class TagsWereAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TagText = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MyEventTag",
                columns: table => new
                {
                    MyEventId = table.Column<int>(nullable: false),
                    TagId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MyEventTag", x => new { x.MyEventId, x.TagId });
                    table.ForeignKey(
                        name: "FK_MyEventTag_MyEvents_MyEventId",
                        column: x => x.MyEventId,
                        principalTable: "MyEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MyEventTag_Tag_TagId",
                        column: x => x.TagId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MyEventTag_TagId",
                table: "MyEventTag",
                column: "TagId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MyEventTag");

            migrationBuilder.DropTable(
                name: "Tag");
        }
    }
}
