using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WikiSystem.Data.Migrations
{
    public partial class UpdateArticleModel2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PublisherId",
                table: "Articles",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Articles_PublisherId",
                table: "Articles",
                column: "PublisherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_AspNetUsers_PublisherId",
                table: "Articles",
                column: "PublisherId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_AspNetUsers_PublisherId",
                table: "Articles");

            migrationBuilder.DropIndex(
                name: "IX_Articles_PublisherId",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "PublisherId",
                table: "Articles");
        }
    }
}
