using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PizzaPortal.Migrations;
using PizzaPortal.WEB.Security.TokenLife;
using System;

namespace PizzaPortal.WEB.Installers
{
    public class IdentityInstaller : IInstaller
    {
        public void InstallerServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedEmail = true;
                options.Tokens.EmailConfirmationTokenProvider = "CustomEmailConfirmation";
            })
                  .AddEntityFrameworkStores<DataContext>()
                  .AddDefaultTokenProviders()
                  .AddTokenProvider<CustomEmailConfirmationTokenProvider<IdentityUser>>("CustomEmailConfirmation");

            services.Configure<DataProtectionTokenProviderOptions>(options =>
            {
                options.TokenLifespan = TimeSpan.FromHours(5);
            });

            services.Configure<CustomEmailConfirmationTokenProviderOptions>(options =>
            {
                options.TokenLifespan = TimeSpan.FromDays(3);
            });
        }
    }
}
