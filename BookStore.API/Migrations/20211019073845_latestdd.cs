using Microsoft.EntityFrameworkCore.Migrations;

namespace BookStore.API.Migrations
{
    public partial class latestdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Auther_autherforignkey",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "autherforignkey",
                table: "Books",
                newName: "autherId");

            migrationBuilder.RenameIndex(
                name: "IX_Books_autherforignkey",
                table: "Books",
                newName: "IX_Books_autherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Auther_autherId",
                table: "Books",
                column: "autherId",
                principalTable: "Auther",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Auther_autherId",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "autherId",
                table: "Books",
                newName: "autherforignkey");

            migrationBuilder.RenameIndex(
                name: "IX_Books_autherId",
                table: "Books",
                newName: "IX_Books_autherforignkey");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Auther_autherforignkey",
                table: "Books",
                column: "autherforignkey",
                principalTable: "Auther",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
