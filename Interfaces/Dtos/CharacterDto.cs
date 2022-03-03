using Microsoft.AspNetCore.Http;

namespace Interfaces
{
    public class CharacterDto : Dto
    {
        public string Name { get; set; }

        public string ImageUrl { get; set; }
    }

    public class CharacterDetailDto : CharacterDto
    {
        public int Age { get; set; }

        public int Weight { get; set; }

        public string Story { get; set; }

        public MovieDto Movie { get; set; }
    }

    public class CharacterCreationDto : Dto
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public int Weight { get; set; }

        public string Story { get; set; }

        public IFormFile Image { get; set; }
    }

}
