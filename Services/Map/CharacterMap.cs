using Domain;
using Interfaces;

namespace Services
{
    public static partial class MapConfig
    {
        public static Character MapToCharacter(this CharacterCreationDto dto)
        {
            return new Character {
                Id = dto.Id,
                Deleted = false,
                Name = dto.Name,
                Age = dto.Age,
                Weight = dto.Weight,
                Story = dto.Story,
                ImageUrl = ""
            };
        }

        public static CharacterDetailDto MapToCharacterDetailDto(this Character dto)
        {
            return new CharacterDetailDto {
                Id = dto.Id,
                Name = dto.Name,
                Age = dto.Age,
                Weight = dto.Weight,
                Story = dto.Story,
                ImageUrl = dto.ImageUrl,
            };
        }

        public static CharacterDto MapToCharacterDto(this Character dto)
        {
            return new CharacterDto {
                Id = dto.Id,
                Name = dto.Name,
                ImageUrl = dto.ImageUrl,
            };
        }
    }
}
