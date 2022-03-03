using Microsoft.AspNetCore.Http;

namespace Interfaces
{
    public class ImageUpdateDto : Dto
    {
        public byte[] Image { get; set; }

        public string Extension { get; set; }
    }
}
