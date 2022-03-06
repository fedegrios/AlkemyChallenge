using Helpers;
using Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AlkemyChallenge.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthorizationController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConfiguration configuration;
        private readonly SignInManager<IdentityUser> signInManager;

        public AuthorizationController(UserManager<IdentityUser> userManager, IConfiguration configuration, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.configuration = configuration;
            this.signInManager = signInManager;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<AuthenticationResult>> Login(UserCredential userCredential)
        {
            try
            {
                var result = await signInManager.PasswordSignInAsync(
                    userCredential.Email,
                    userCredential.Password,
                    isPersistent: false,
                    lockoutOnFailure: false
                );

                if (!result.Succeeded)
                    BadRequest("No se pudo ingresar al sistema.");

                return BuildAuthenticationResult(userCredential);
            }
            catch (Exception e)
            {
                Console.WriteLine($"AuthorizationController.Register: {e.Message}");
                return BadRequest("No se pudo registrar al usuario.");
            }
        }


        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<AuthenticationResult>> Register(UserCredential userCredential)
        {
            try
            {
                var user = new IdentityUser() { 
                    UserName = userCredential.Email,
                    Email = userCredential.Email
                };

                var result = await userManager.CreateAsync(user, userCredential.Password);

                if (!result.Succeeded)
                    return BadRequest(result.Errors);

                return BuildAuthenticationResult(userCredential);
            }
            catch (Exception e)
            {
                Console.WriteLine($"AuthorizationController.Register: {e.Message}");
                return BadRequest("No se pudo registrar al usuario.");
            }
        }

        private AuthenticationResult BuildAuthenticationResult(UserCredential userCredential)
        {
            var claims = new List<Claim> { 
                new Claim("email", userCredential.Email)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expirationDateTime = AppConfiguration.GetTokenExpirationDate;

            var securityToken = new JwtSecurityToken(
                issuer: null, 
                audience: null,
                claims: claims,
                expires: expirationDateTime,
                signingCredentials: creds
            );

            var token = new JwtSecurityTokenHandler().WriteToken(securityToken);

            Console.WriteLine(token);

            return new AuthenticationResult
            {
                ExpirationDate = expirationDateTime,
                Token = token
            };
        }
    }
}
