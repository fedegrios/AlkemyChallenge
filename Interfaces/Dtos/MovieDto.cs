using System;
using System.Collections.Generic;

namespace Interfaces
{
    public class MovieDto : Dto
    {
        public string Title { get; set; }

        public string ImageUrl { get; set; }

        public DateTime CreationDate { get; set; }
    }

    public class MovieCreationDto : MovieDto
    {
        public int Score { get; set; }

        public List<int> CharacterIds { get; set; }

        public List<int> GenreIds { get; set; }
    }

    public class MovieDetailDto : MovieDto
    {
        public List<CharacterDto> CharactersMuvie { get; set; }
            = new List<CharacterDto>();

        public List<string> Genres { get; set; }
            =new List<string>();
    }
}
