using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddRecBook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecommendedBooks_Books_BookId",
                table: "RecommendedBooks");

            migrationBuilder.DropIndex(
                name: "IX_RecommendedBooks_BookId",
                table: "RecommendedBooks");

            migrationBuilder.DropColumn(
                name: "BookId",
                table: "RecommendedBooks");

            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "RecommendedBooks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "RecommendedBooks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "RecommendedBooks",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Author",
                table: "RecommendedBooks");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "RecommendedBooks");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "RecommendedBooks");

            migrationBuilder.AddColumn<string>(
                name: "BookId",
                table: "RecommendedBooks",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_RecommendedBooks_BookId",
                table: "RecommendedBooks",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_RecommendedBooks_Books_BookId",
                table: "RecommendedBooks",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
