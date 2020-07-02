using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PizzaPortal.BLL.Services.Abstract;
using PizzaPortal.BLL.Services.Concrete;
using PizzaPortal.BLL.Settings;
using PizzaPortal.DAL.Repositories.Abstract;
using PizzaPortal.DAL.Repositories.Concrete;
using PizzaPortal.Model.Models;
using PizzaPortal.WEB.Security.Policy.Admin;

namespace PizzaPortal.WEB.Installers
{
    public class ServiceInstaller : IInstaller
    {
        public void InstallerServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IRepository<BaseModel>, Repository<BaseModel>>();

            services.AddScoped<IPizzaRepository, PizzaRepository>();

            services.AddScoped<ICategoryRepository, CategoryRepository>();

            services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();

            services.AddScoped<IOrderRepository, OrderRepository>();

            services.AddScoped<IService<BaseModel>, Service<BaseModel>>();

            services.AddScoped<IPizzaService, PizzaService>();

            services.AddScoped<ICategoryService, CategoryService>();

            services.AddScoped<IShoppingCartService, ShoppingCartService>();

            services.AddScoped<IOrderService, OrderService>();

            services.AddScoped<IIngredientService, IngredientService>();

            services.AddScoped<IIngredientRepository, IngredientRepository>();

            services.AddScoped<IPizzaIngredientService, PizzaIngredientService>();

            services.AddScoped<IPizzaIngredientRepository, PizzaIngredientRepository>();

            services.AddScoped<IShoppingCartService, ShoppingCartService>();

            services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>(sp => ShoppingCartRepository.GetCart(sp));

            services.AddTransient<IEmailService, EmailService>();

            services.AddSingleton<IEmailConfiguration>(configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>());
        }
    }
}
