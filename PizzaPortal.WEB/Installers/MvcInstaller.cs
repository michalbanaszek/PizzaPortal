using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PizzaPortal.WEB.Installers
{
    public class MvcInstaller : IInstaller
    {
        public void InstallerServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddMvc(options =>
            {
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser()
                                                             .Build();

                options.Filters.Add(new AuthorizeFilter(policy));

            }).AddFluentValidation(conf =>
              {
                  conf.LocalizationEnabled = false;
                  conf.RegisterValidatorsFromAssemblyContaining<Startup>();
              });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }
    }
}
