using Infraestructure;
using Interfaces;
using Services;

namespace AlkemyChallenge
{
    public class Startup
    {
        public void ConfigIoC(IServiceCollection services)
        {
            services.AddSingleton<DataContext>();

            services.AddTransient<IStorageServices, LocalStorageServices>();

        }
    }
}
