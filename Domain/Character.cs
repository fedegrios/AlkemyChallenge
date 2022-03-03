using System.Collections.Generic;

namespace Domain
{
    public class Character : Entity
    {
        public string ImageUrl { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public int Weight { get; set; }

        public string Story { get; set; }

        public List<CharacterMovie> CharacterMovies { get; set; }


    }
}
