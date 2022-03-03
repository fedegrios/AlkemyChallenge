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

        public List<CharacterMovie> CharactersMuvie { get; set; }

        public List<GenreMovie> GenresMuvie { get; set; }
    }
}
