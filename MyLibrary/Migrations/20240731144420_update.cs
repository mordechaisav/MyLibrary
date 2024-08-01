using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyLibrary.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Book_BookSet_BookSetId",
                table: "Book");

            migrationBuilder.DropForeignKey(
                name: "FK_Book_Library_LibraryId",
                table: "Book");

            migrationBuilder.AlterColumn<int>(
                name: "LibraryId",
                table: "Book",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "BookSetId",
                table: "Book",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Book_BookSet_BookSetId",
                table: "Book",
                column: "BookSetId",
                principalTable: "BookSet",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Library_LibraryId",
                table: "Book",
                column: "LibraryId",
                principalTable: "Library",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Book_BookSet_BookSetId",
                table: "Book");

            migrationBuilder.DropForeignKey(
                name: "FK_Book_Library_LibraryId",
                table: "Book");

            migrationBuilder.AlterColumn<int>(
                name: "LibraryId",
                table: "Book",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BookSetId",
                table: "Book",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Book_BookSet_BookSetId",
                table: "Book",
                column: "BookSetId",
                principalTable: "BookSet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Library_LibraryId",
                table: "Book",
                column: "LibraryId",
                principalTable: "Library",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
