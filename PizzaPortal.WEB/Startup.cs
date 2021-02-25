using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PizzaPortal.Migrations.Initializer;
using PizzaPortal.WEB.Installers;
using Microsoft.Extensions.Hosting;
using System;

namespace PizzaPortal.WEB
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            services.InstallServicesInAssembly(this.Configuration);
        }
 
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider service)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
            }

            app.UseStaticFiles();
            app.UseSession();
            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(routes =>
            {                
                routes.MapControllerRoute(name: "categoryFilter", "Pizza/{action}/{category?}", defaults: new { controller = "Pizza", action = "Index" });
                routes.MapControllerRoute(name: "default", "{controller=Home}/{action=Index}/{id?}");
            });

            DbInitializer.Initialize(service);
        }
    }
}
