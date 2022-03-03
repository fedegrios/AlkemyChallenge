using Domain;
using Interfaces;

namespace Services
{
    public static partial class MapConfig
    {
        public static GenreDto MapToGenreDto(this Genre ent)
        {
            return new GenreDto { 
                Id = ent.Id,
                Name = ent.Name,
                ImageUrl = ent.ImageUrl,
            };
        }
    }
}
