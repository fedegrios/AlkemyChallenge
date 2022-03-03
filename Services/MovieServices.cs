using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Infraestructure;
using Interfaces;
using Helpers;
using System.IO;

namespace Services
{
    public class MovieServices : IMovieServices
    {
        private readonly DataContext dataContext;

        public MovieServices(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<int> Create(MovieCreationDto dto)
        {
            try
            {
                var allCharactersExists = dto.CharacterIds.Count > 0
                    || dto.CharacterIds.All(id => dataContext.Characters.Any(c => c.Id == id));

                var allGenresExists = dto.GenreIds.Count > 0 
                    || dto.GenreIds.All(id => dataContext.Genres.Any(g => g.Id == id));

                if (!allCharactersExists || !allGenresExists)
                {
                    Console.WriteLine($@"MovieServices.Create: some characters or genres not exists.");
                    throw new Exception();
                }

                var newCharacter = dto.MapToMovie();

                dataContext.Movies.Add(newCharacter);

                await dataContext.SaveChangesAsync();

                dto.CharacterIds.ForEach(x => dataContext.CharacterMovies.Add(
                    new Domain.CharacterMovie { 
                        CharacterId = x,
                        MovieId = newCharacter.Id
                    }
                ));

                dto.GenreIds.ForEach(x => dataContext.GenreMovies.Add( 
                    new Domain.GenreMovie { 
                        GenreId = x,
                        MovieId = newCharacter.Id
                    }
                ));

                dataContext.SaveChanges();

                return newCharacter.Id;
            }
            catch (Exception e)
            {
                Console.WriteLine($@"MovieServices.Create: {e.Message}");
                throw;
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                if (!dataContext.Movies.Any(x => x.Id == id))
                    return false;

                var movie = dataContext.Movies.FirstOrDefault(x => x.Id == id);

                if (movie == null)
                    return false;

                movie.Deleted = true;

                await dataContext.SaveChangesAsync();

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"MovieServices.Delete: {e.Message}");
                return false;
            }
        }

        public async Task<bool> Update(MovieCreationDto dto)
        {
            try
            {
                var movie = await dataContext.Movies.FirstOrDefaultAsync(x => x.Id == dto.Id);

                if (movie == null)
                    return false;

                movie.Title = dto.Title;
                movie.Score = dto.Score;
                movie.CreationDate = dto.CreationDate;

                await dataContext.SaveChangesAsync();

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"MovieServices.Update: {e.Message}");
                throw;
            }
        }

        public async Task<MovieDetailDto> Get(int id)
        {
            try
            {
                if (!dataContext.Movies.Any(x => x.Id == id))
                    return null;

                var Movie = dataContext.Movies
                    .Include(x => x.CharactersMovie)
                    .Include(x => x.GenresMovie)
                    .AsNoTracking()
                    .FirstOrDefault(x => x.Id == id);

                if (Movie == null)
                    return null;

                return Movie.MapToMovieDetailDto();
            }
            catch (Exception e)
            {
                Console.WriteLine($"MovieServices.Get: {e.Message}");
                throw;
            }
        }

        public async Task<List<MovieDto>> List(string name, int genreId, string order)
        {
            try
            {
                var lstMovies = await dataContext.Movies
                    .Include(x => x.CharactersMovie)
                    .Include(x => x.GenresMovie)
                    .AsNoTracking()
                    .Where(x => !x.Deleted && x.Title.Contains(name))
                    .ToListAsync();

                if (genreId > 0)
                    lstMovies = lstMovies
                        .Where(x => x.GenresMovie.Any(cm => cm.GenreId == genreId))
                        .ToList();

                if (order.ToLower() == "desc")
                    lstMovies = lstMovies.OrderByDescending(x => x.CreationDate).ToList();

                else
                    lstMovies = lstMovies.OrderBy(x => x.CreationDate).ToList();

                return lstMovies
                    .Select(x => x.MapToMovieDto())
                    .ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine($@"MovieServices.List: {e.Message}");
                throw;
            }
        }

        public async Task<string> UpdateImage(ImageUpdateDto dto)
        {
            var movie = await dataContext.Movies.FirstOrDefaultAsync(x => x.Id == dto.Id);

            if (movie == null)
                return "";

            var storageServices = new LocalStorageServices();
            var webRootPath = AppConfiguration.WebRootPath;

            if (!string.IsNullOrEmpty(movie.ImageUrl))
                await storageServices.Delete(movie.ImageUrl, webRootPath);

            var destinationFolder = Path.Combine(webRootPath, "movies");
            var generatedImageUrl = await storageServices.Save(dto.Image, dto.Extension, destinationFolder);

            movie.ImageUrl = Path.Combine("movies", generatedImageUrl);

            await dataContext.SaveChangesAsync();

            return generatedImageUrl;
        }
    }
}
