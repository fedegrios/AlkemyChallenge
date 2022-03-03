using Helpers;
using Infraestructure;
using Interfaces;
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

        public void ConfigIoC()
        {
            services.AddSingleton<DataContext>();

            services.AddTransient<IStorageServices, LocalStorageServices>();
            services.AddTransient<ICharacterServices, CharacterServices>();
            services.AddTransient<IMovieServices, MovieServices>();

        }
    }
}
