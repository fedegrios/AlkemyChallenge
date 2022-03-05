using Helpers;
using Infraestructure;
using Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Services;

namespace AlkemyChallenge
{
    public class Startup
    {
        private readonly IServiceCollection services;

        public Startup(IConfiguration config, IServiceCollection services, IWebHostEnvironment env)
        {
            this.services = services;

            AppConfiguration.ConnectionString = config.GetConnectionString("DefaultConnection");
            AppConfiguration.WebRootPath = env.WebRootPath;

        }

        public void ConfigServices()
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer();

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
