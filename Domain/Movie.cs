using System;
using System.Collections.Generic;

namespace Domain
{
    public class Movie : Entity
    {
        public string ImageUrl { get; set; }

        public string Title { get; set; }

        public DateTime CreationDate { get; set; }

        public int Score { get; set; }

        public List<CharacterMovie> CharactersMovie { get; set; }

        public List<GenreMovie> GenresMovie { get; set; }
    }
}
