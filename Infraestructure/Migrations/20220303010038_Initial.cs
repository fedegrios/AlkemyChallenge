using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CharacterMovies",
                columns: table => new
                {
                    CharacterId = table.Column<int>(type: "int", nullable: false),
                    MovieId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterMovies", x => new { x.CharacterId, x.MovieId });
                });

            migrationBuilder.CreateTable(
                name: "Characters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Story = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GenreMovies",
                columns: table => new
                {
                    GenreId = table.Column<int>(type: "int", nullable: false),
                    MovieId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenreMovies", x => new { x.GenreId, x.MovieId });
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CharacterCharacterMovie",
                columns: table => new
                {
                    CharactersId = table.Column<int>(type: "int", nullable: false),
                    CharacterMoviesCharacterId = table.Column<int>(type: "int", nullable: false),
                    CharacterMoviesMovieId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterCharacterMovie", x => new { x.CharactersId, x.CharacterMoviesCharacterId, x.CharacterMoviesMovieId });
                    table.ForeignKey(
                        name: "FK_CharacterCharacterMovie_CharacterMovies_CharacterMoviesCharacterId_CharacterMoviesMovieId",
                        columns: x => new { x.CharacterMoviesCharacterId, x.CharacterMoviesMovieId },
                        principalTable: "CharacterMovies",
                        principalColumns: new[] { "CharacterId", "MovieId" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterCharacterMovie_Characters_CharactersId",
                        column: x => x.CharactersId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GenreGenreMovie",
                columns: table => new
                {
                    GenresId = table.Column<int>(type: "int", nullable: false),
                    GenreMoviesGenreId = table.Column<int>(type: "int", nullable: false),
                    GenreMoviesMovieId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenreGenreMovie", x => new { x.GenresId, x.GenreMoviesGenreId, x.GenreMoviesMovieId });
                    table.ForeignKey(
                        name: "FK_GenreGenreMovie_GenreMovies_GenreMoviesGenreId_GenreMoviesMovieId",
                        columns: x => new { x.GenreMoviesGenreId, x.GenreMoviesMovieId },
                        principalTable: "GenreMovies",
                        principalColumns: new[] { "GenreId", "MovieId" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GenreGenreMovie_Genres_GenresId",
                        column: x => x.GenresId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CharacterMovieMovie",
                columns: table => new
                {
                    MoviesId = table.Column<int>(type: "int", nullable: false),
                    CharactersMuvieCharacterId = table.Column<int>(type: "int", nullable: false),
                    CharactersMuvieMovieId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterMovieMovie", x => new { x.MoviesId, x.CharactersMuvieCharacterId, x.CharactersMuvieMovieId });
                    table.ForeignKey(
                        name: "FK_CharacterMovieMovie_CharacterMovies_CharactersMuvieCharacterId_CharactersMuvieMovieId",
                        columns: x => new { x.CharactersMuvieCharacterId, x.CharactersMuvieMovieId },
                        principalTable: "CharacterMovies",
                        principalColumns: new[] { "CharacterId", "MovieId" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterMovieMovie_Movies_MoviesId",
                        column: x => x.MoviesId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GenreMovieMovie",
                columns: table => new
                {
                    MoviesId = table.Column<int>(type: "int", nullable: false),
                    GenresMuvieGenreId = table.Column<int>(type: "int", nullable: false),
                    GenresMuvieMovieId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenreMovieMovie", x => new { x.MoviesId, x.GenresMuvieGenreId, x.GenresMuvieMovieId });
                    table.ForeignKey(
                        name: "FK_GenreMovieMovie_GenreMovies_GenresMuvieGenreId_GenresMuvieMovieId",
                        columns: x => new { x.GenresMuvieGenreId, x.GenresMuvieMovieId },
                        principalTable: "GenreMovies",
                        principalColumns: new[] { "GenreId", "MovieId" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GenreMovieMovie_Movies_MoviesId",
                        column: x => x.MoviesId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CharacterCharacterMovie_CharacterMoviesCharacterId_CharacterMoviesMovieId",
                table: "CharacterCharacterMovie",
                columns: new[] { "CharacterMoviesCharacterId", "CharacterMoviesMovieId" });

            migrationBuilder.CreateIndex(
                name: "IX_CharacterMovieMovie_CharactersMuvieCharacterId_CharactersMuvieMovieId",
                table: "CharacterMovieMovie",
                columns: new[] { "CharactersMuvieCharacterId", "CharactersMuvieMovieId" });

            migrationBuilder.CreateIndex(
                name: "IX_GenreGenreMovie_GenreMoviesGenreId_GenreMoviesMovieId",
                table: "GenreGenreMovie",
                columns: new[] { "GenreMoviesGenreId", "GenreMoviesMovieId" });

            migrationBuilder.CreateIndex(
                name: "IX_GenreMovieMovie_GenresMuvieGenreId_GenresMuvieMovieId",
                table: "GenreMovieMovie",
                columns: new[] { "GenresMuvieGenreId", "GenresMuvieMovieId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CharacterCharacterMovie");

            migrationBuilder.DropTable(
                name: "CharacterMovieMovie");

            migrationBuilder.DropTable(
                name: "GenreGenreMovie");

            migrationBuilder.DropTable(
                name: "GenreMovieMovie");

            migrationBuilder.DropTable(
                name: "Characters");

            migrationBuilder.DropTable(
                name: "CharacterMovies");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "GenreMovies");

            migrationBuilder.DropTable(
                name: "Movies");
        }
    }
}
