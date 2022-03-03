using System.Collections.Generic;

namespace Domain
{
    public class GenreMovie
    {
        public int GenreId { get; set; }

        public int MovieId { get; set; }

        public List<Genre> Genres { get; set; }

        public List<Movie> Movies { get; set; }
    }
}
