using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PizzaPortal.WEB.Installers
{
    public interface IInstaller
    {
        void InstallerServices(IServiceCollection services, IConfiguration configuration);
    }
}
