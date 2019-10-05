using Microsoft.EntityFrameworkCore.Migrations;

namespace MicroBlog.Migrations
{
    public partial class Relation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "userId",
                table: "posts",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_posts_userId",
                table: "posts",
                column: "userId");

           /* migrationBuilder.AddForeignKey(
                name: "FK_posts_AspNetUsers_userId",
                table: "posts",
                column: "userId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);*/
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_posts_AspNetUsers_userId",
                table: "posts");

            migrationBuilder.DropIndex(
                name: "IX_posts_userId",
                table: "posts");

            migrationBuilder.DropColumn(
                name: "userId",
                table: "posts");
        }
    }
}
