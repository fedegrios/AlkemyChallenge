using System.ComponentModel.DataAnnotations;

namespace Interfaces
{
    public class UserCredential
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MaxLength(140)]
        public string Password { get; set; }
    }
}
