using System.Threading.Tasks;

namespace Interfaces
{
    public interface IStorageServices
    {
        Task<string> Save(byte[] data, string extension, string folder);

        Task<bool> Delete(string path, string cointeiner);
    }
}
