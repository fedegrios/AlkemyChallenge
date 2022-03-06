using Helpers;
using Infraestructure;
using Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Services;
using System.Text;

namespace AlkemyChallenge
{
    public class Startup
    {
        private readonly IConfiguration config;
        private readonly IServiceCollection services;
        private readonly IWebHostEnvironment env;

        public Startup(IConfiguration config, IServiceCollection services, IWebHostEnvironment env)
        {
            this.config = config;
            this.services = services;
            this.env = env;
            AppConfiguration.ConnectionString = config.GetConnectionString("DefaultConnection");
            AppConfiguration.WebRootPath = env.WebRootPath;

        }

        public void ConfigServices()
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtKey"]));
            var tokenValidationParameters = new TokenValidationParameters { 
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
                ClockSkew = TimeSpan.Zero,
            };

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(
                    options => options.TokenValidationParameters= tokenValidationParameters
                );

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<DataContext>()
                .AddDefaultTokenProviders();
        }

        public void ConfigIoC()
        {
            services.AddSingleton<DataContext>();

            services.AddTransient<IStorageServices, LocalStorageServices>();
            services.AddTransient<ICharacterServices, CharacterServices>();
            services.AddTransient<IMovieServices, MovieServices>();

        }
    }
}
