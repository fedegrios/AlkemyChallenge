using System.Collections.Generic;

namespace Domain
{
    public class GenreMovie
    {
        public int GenreId { get; set; }

        public int MovieId { get; set; }

        public Genre Genre { get; set; }

        public Movie Movie { get; set; }
    }
}
