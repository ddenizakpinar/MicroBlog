using Microsoft.EntityFrameworkCore.Migrations;

namespace MicroBlog.Migrations
{
    public partial class image : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.DropIndex(
                name: "IX_posts_categoryid",
                table: "posts");

            migrationBuilder.RenameColumn(
                name: "categoryid",
                table: "posts",
                newName: "categoryId");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "posts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "posts");

            migrationBuilder.RenameColumn(
                name: "categoryId",
                table: "posts",
                newName: "categoryid");

            migrationBuilder.CreateIndex(
                name: "IX_posts_categoryid",
                table: "posts",
                column: "categoryid");

            migrationBuilder.AddForeignKey(
                name: "FK_posts_categories_categoryid",
                table: "posts",
                column: "categoryid",
                principalTable: "categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
