using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class CorrectMovieGenreTableName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieTable_Genre_GenreId",
                table: "MovieTable");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieTable_Movie_MovieId",
                table: "MovieTable");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MovieTable",
                table: "MovieTable");

            migrationBuilder.RenameTable(
                name: "MovieTable",
                newName: "MovieGenre");

            migrationBuilder.RenameIndex(
                name: "IX_MovieTable_GenreId",
                table: "MovieGenre",
                newName: "IX_MovieGenre_GenreId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MovieGenre",
                table: "MovieGenre",
                columns: new[] { "MovieId", "GenreId" });

            migrationBuilder.AddForeignKey(
                name: "FK_MovieGenre_Genre_GenreId",
                table: "MovieGenre",
                column: "GenreId",
                principalTable: "Genre",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MovieGenre_Movie_MovieId",
                table: "MovieGenre",
                column: "MovieId",
                principalTable: "Movie",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieGenre_Genre_GenreId",
                table: "MovieGenre");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieGenre_Movie_MovieId",
                table: "MovieGenre");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MovieGenre",
                table: "MovieGenre");

            migrationBuilder.RenameTable(
                name: "MovieGenre",
                newName: "MovieTable");

            migrationBuilder.RenameIndex(
                name: "IX_MovieGenre_GenreId",
                table: "MovieTable",
                newName: "IX_MovieTable_GenreId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MovieTable",
                table: "MovieTable",
                columns: new[] { "MovieId", "GenreId" });

            migrationBuilder.AddForeignKey(
                name: "FK_MovieTable_Genre_GenreId",
                table: "MovieTable",
                column: "GenreId",
                principalTable: "Genre",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MovieTable_Movie_MovieId",
                table: "MovieTable",
                column: "MovieId",
                principalTable: "Movie",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
