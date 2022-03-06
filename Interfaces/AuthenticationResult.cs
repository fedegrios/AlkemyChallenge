using System;

namespace Interfaces
{
    public class AuthenticationResult
    {
        public string Token { get; set; }

        public DateTime ExpirationDate { get; set; }
    }
}
