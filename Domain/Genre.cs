using System.Collections.Generic;

namespace Domain
{
    public class Genre : Entity
    {
        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public List<GenreMovie> GenreMovies { get; set; }
    }
}
