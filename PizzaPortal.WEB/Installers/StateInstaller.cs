using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PizzaPortal.WEB.Installers
{
    public class StateInstaller : IInstaller
    {
        public void InstallerServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddMemoryCache();
            services.AddSession();
        }
    }
}
