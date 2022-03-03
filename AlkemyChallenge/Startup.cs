using Helpers;
using Infraestructure;
using Interfaces;
using Services;

namespace AlkemyChallenge
{
    public class Startup
    {
        private readonly IConfiguration config;
        private readonly IServiceCollection services;

        public Startup(IConfiguration config, IServiceCollection services)
        {
            this.config = config;
            this.services = services;
        }

        public void ConfigDataBaseConnection()
        {
            AppConfiguration.ConnectionString = config.GetConnectionString("DefaultConnection");
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
