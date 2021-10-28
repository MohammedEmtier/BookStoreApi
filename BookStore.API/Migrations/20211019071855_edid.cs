using Microsoft.EntityFrameworkCore.Migrations;

namespace BookStore.API.Migrations
{
    public partial class edid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "autherforignkey",
                table: "Books",
                type: "tinyint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Books_autherforignkey",
                table: "Books",
                column: "autherforignkey");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Auther_autherforignkey",
                table: "Books",
                column: "autherforignkey",
                principalTable: "Auther",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Auther_autherforignkey",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_autherforignkey",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "autherforignkey",
                table: "Books");
        }
    }
}
