using System.Collections.Generic;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IMovieServices
    {
        Task<List<MovieDto>> List(string name, int genre, string order);

        Task<MovieDetailDto> Get(int id);

        Task<int> Create(MovieCreationDto dto);

        Task<bool> Update(MovieCreationDto dto);

        Task<bool> Delete(int id);

        Task<string> UpdateImage(ImageUpdateDto updateImageDto);
    }
}
