namespace Interfaces
{
    public class CharacterDto : Dto
    {
        public string Name { get; set; }

        public string ImageUrl { get; set; }
    }

    public class CharacterCreationDto : CharacterDto
    {
        public int Age { get; set; }

        public int Weight { get; set; }

        public string Story { get; set; }
    }

    public class CharacterDetailDto : CharacterCreationDto
    {
        public MovieDto Movie { get; set; }
    }

}
