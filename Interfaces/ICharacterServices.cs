using System.Threading.Tasks;

namespace Interfaces
{
    public interface ICharacterServices
    {
        Task<CharacterDetailDto> Get(int id);

        Task<CharacterDto> List(string name, int age, int movieId);

        Task<int> Create(CharacterCreationDto dto);

        Task<bool> Update(CharacterCreationDto dto);

        Task<bool> Delete(int id);

        Task<string> UpdateImage(ImageUpdateDto dto);
    }
}
