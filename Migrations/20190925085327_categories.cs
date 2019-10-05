using Microsoft.EntityFrameworkCore.Migrations;

namespace MicroBlog.Migrations
{
    public partial class categories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "categoryid",
                table: "posts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categories", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_posts_categoryid",
                table: "posts",
                column: "categoryid");

           /* migrationBuilder.AddForeignKey(
                name: "FK_posts_categories_categoryid",
                table: "posts",
                column: "categoryid",
                principalTable: "categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);*/
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_posts_categories_categoryid",
                table: "posts");

            migrationBuilder.DropTable(
                name: "categories");

            migrationBuilder.DropIndex(
                name: "IX_posts_categoryid",
                table: "posts");

            migrationBuilder.DropColumn(
                name: "categoryid",
                table: "posts");
        }
    }
}
