using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PizzaPortal.WEB.Installers
{
    public class AuthenticationInstaller : IInstaller
    {
        public void InstallerServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication()
                   .AddGoogle(options =>
                   {
                       options.ClientId = configuration.GetSection("ExternalLogin:Google:ClientId").Value;
                       options.ClientSecret = configuration.GetSection("ExternalLogin:Google:ClientSecret").Value;
                   })
                   .AddFacebook(options =>
                   {
                       options.AppId = configuration.GetSection("ExternalLogin:Facebook:ClientId").Value;
                       options.AppSecret = configuration.GetSection("ExternalLogin:Facebook:ClientSecret").Value;
                   });
        }
    }
}
