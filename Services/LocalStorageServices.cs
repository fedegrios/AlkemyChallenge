using System;
using System.IO;
using System.Threading.Tasks;
using Interfaces;

namespace Services
{
    public class LocalStorageServices : IStorageServices
    {
        public async Task<string> Save(byte[] data, string extension, string webRootPath)
        {
            var fileName = $"{Guid.NewGuid()}{extension}";

            if(!Directory.Exists(webRootPath))
                Directory.CreateDirectory(webRootPath);

            await File.WriteAllBytesAsync(Path.Combine(webRootPath, fileName), data);

            return Path.Combine(webRootPath, fileName).Replace("\\","/");
        }

        public async Task<bool> Delete(string path, string webRootPath)
        {
            if (!string.IsNullOrEmpty(path))
                return await Task.FromResult(false);

            var fileName = Path.GetFileName(path);
            string pathFolder = Path.Combine(webRootPath, fileName);

            if(!File.Exists(pathFolder))
                return await Task.FromResult(false);

            File.Delete(pathFolder);

            return await Task.FromResult(true);
        }

    }
}
