using Interfaces;
using Services;

namespace AlkemyChallenge
{
    public class Startup
    {
        public void ConfigIoC(IServiceCollection services)
        {
            services.AddTransient<IStorageServices, LocalStorageServices>();
        }
    }
}
