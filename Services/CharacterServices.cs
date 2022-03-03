using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.IO;
using Infraestructure;
using Interfaces;
using System;
using Helpers;

namespace Services
{
    public class CharacterServices : ICharacterServices
    {
        private readonly DataContext dataContext;

        public CharacterServices(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<int> Create(CharacterCreationDto dto)
        {
            try
            {
                var newCharacter = dto.MapToCharacter();

                dataContext.Characters.Add(newCharacter);

                await dataContext.SaveChangesAsync();

                return newCharacter.Id;
            }
            catch (Exception e)
            {
                Console.WriteLine($@"CharacterServices.Create: {e.Message}");
                throw;
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                if (!dataContext.Characters.Any(x => x.Id == id))
                    return false;

                var character = dataContext.Characters.FirstOrDefault(x => x.Id == id);

                if (character == null)
                    return false;

                character.Deleted = true;

                await dataContext.SaveChangesAsync();

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"CharacterServices.Delete: {e.Message}");
                return false;
            }
        }

        public async Task<bool> Update(CharacterCreationDto dto)
        {
            try
            {
                var character = await dataContext.Characters.FirstOrDefaultAsync(x => x.Id == dto.Id);

                if (character == null)
                    return false;

                character.Name = dto.Name;
                character.Age = dto.Age;
                character.Weight = dto.Weight;
                character.Story = dto.Story;

                await dataContext.SaveChangesAsync();

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"CharacterServices.Update: {e.Message}");
                throw;
            }
        }

        public async Task<CharacterDetailDto> Get(int id)
        {
            try
            {
                if (!dataContext.Characters.Any(x => x.Id == id))
                    return null;

                var character = dataContext.Characters
                    .AsNoTracking()
                    .FirstOrDefault(x => x.Id == id);

                if (character == null)
                    return null;

                return character.MapToCharacterDetailDto();
            }
            catch (Exception e)
            {
                Console.WriteLine($"CharacterServices.Get: {e.Message}");
                throw;
            }
        }

        public async Task<List<CharacterDto>> List(string name, int age, int movieId)
        {
            try
            {
                var lstCharacters = await dataContext.Characters
                    .Include(x => x.CharacterMovies)
                    .AsNoTracking()
                    .Where(x => !x.Deleted && x.Name.Contains(name))
                    .ToListAsync();

                if (age > 0)
                    lstCharacters = lstCharacters
                        .Where(x => x.Age == age)
                        .ToList();

                if (movieId > 0)
                    lstCharacters = lstCharacters
                        .Where(x => x.CharacterMovies.Any(cm => cm.MovieId == movieId))
                        .ToList();

                return lstCharacters
                    .Select(x => x.MapToCharacterDto())
                    .ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine($@"CharacterServices.List: {e.Message}");
                throw;
            }
        }

        public async Task<string> UpdateImage(ImageUpdateDto dto)
        {
            var character = await dataContext.Characters.FirstOrDefaultAsync(x => x.Id == dto.Id);

            if (character == null)
                return "";

            var storageServices = new LocalStorageServices();
            var webRootPath = AppConfiguration.WebRootPath;

            if (!string.IsNullOrEmpty(character.ImageUrl))
                await storageServices.Delete(character.ImageUrl, webRootPath);

            var destinationFolder = Path.Combine(webRootPath, "characters");
            var generatedImageUrl = await storageServices.Save(dto.Image, dto.Extension, destinationFolder);

            character.ImageUrl = Path.Combine("characters", generatedImageUrl);

            await dataContext.SaveChangesAsync();

            return generatedImageUrl;
        }
    }
}
