using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PizzaPortal.Migrations;

namespace PizzaPortal.WEB.Installers
{
    public class DatabaseInstaller : IInstaller
    {
        public void InstallerServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(configuration["ConnectionStrings:DockerConnection"]);
            });
        }
    }
}
