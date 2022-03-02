using Microsoft.AspNetCore.Http;

namespace Interfaces
{
    public class ImageUpdateDto : Dto
    {
        public IFormFile Image { get; set; }
    }
}
