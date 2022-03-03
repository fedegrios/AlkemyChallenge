using System.Collections.Generic;

namespace Interfaces
{
    public class CharacterDto : Dto
    {
        public string Name { get; set; }

        public string ImageUrl { get; set; }
    }

    public class CharacterCreationDto : CharacterDto
    {
        public string Age { get; set; }

        public string Weight { get; set; }

        public string Story { get; set; }
    }

    public class CharacterDetailDto : CharacterCreationDto
    {
        public MovieDto Movie { get; set; }
    }

}
