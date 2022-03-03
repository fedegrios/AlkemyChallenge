using System.Collections.Generic;

namespace Domain
{
    public class CharacterMovie
    {
        public int CharacterId { get; set; }

        public int MovieId { get; set; }

        public List<Character> Characters { get; set; }

        public List<Movie> Movies { get; set; }
    }
}
