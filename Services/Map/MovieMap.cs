using Domain;
using Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Services
{
    public static partial class MapConfig
    {
        public static Movie MapToMovie(this MovieCreationDto dto)
        {
            return new Movie {
                Id = dto.Id,
                Deleted = false,
                ImageUrl = "",
                Title = dto.Title,
                Score = dto.Score,
                CreationDate = dto.CreationDate,
            };
        }

        public static MovieDto MapToMovieDto(this Movie ent)
        {
            return new MovieDto {
                Id = ent.Id,
                ImageUrl = ent.ImageUrl,
                Title = ent.Title,
                CreationDate = ent.CreationDate,
            };
        }

        public static MovieDetailDto MapToMovieDetailDto(this Movie ent)
        {
            var lstCharacters = new List<CharacterDto>();
            var lstGenres = new List<GenreDto>();

            if (ent.CharactersMovie != null)
                lstCharacters = ent.CharactersMovie
                    .Select(x => x.Character.MapToCharacterDto())
                    .ToList();

            if (ent.GenresMovie != null)
                lstGenres = ent.GenresMovie
                    .Select(x => x.Genre.MapToGenreDto())
                    .ToList();

            return new MovieDetailDto { 
                Id = ent.Id,
                Title = ent.Title,
                Score = ent.Score,
                ImageUrl = ent.ImageUrl,
                Characters = lstCharacters,
                Genres = lstGenres
            };
        }
    }
}
