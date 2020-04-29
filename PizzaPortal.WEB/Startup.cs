using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PizzaPortal.BLL.Services.Abstract;
using PizzaPortal.BLL.Services.Concrete;
using PizzaPortal.BLL.Settings;
using PizzaPortal.DAL.Repositories.Abstract;
using PizzaPortal.DAL.Repositories.Concrete;
using PizzaPortal.Migrations;
using PizzaPortal.Migrations.Initializer;
using PizzaPortal.Model.Models;
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
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("PizzaConnection"));
            });

            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedEmail = true;
            })
                    .AddEntityFrameworkStores<DataContext>()
                    .AddDefaultTokenProviders();

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
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IShoppingCartService, ShoppingCartService>();
            services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>(sp => ShoppingCartRepository.GetCart(sp));
            services.AddSingleton<IEmailConfiguration>(Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>());
            services.AddTransient<IEmailService, EmailService>();

            services.AddMvc(options =>
            {
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser()
                                                             .Build();

                options.Filters.Add(new AuthorizeFilter(policy));
            })
              .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
              .AddFluentValidation(conf =>
              {
                  conf.LocalizationEnabled = false;
                  conf.RegisterValidatorsFromAssemblyContaining<Startup>();
              });

            services.AddAuthentication()
                    .AddGoogle(options =>
                    {
                        options.ClientId = this.Configuration.GetSection("ExternalLogin:Google:ClientId").Value;
                        options.ClientSecret = this.Configuration.GetSection("ExternalLogin:Google:ClientSecret").Value;
                    })
                    .AddFacebook(options =>
                     {
                         options.AppId = this.Configuration.GetSection("ExternalLogin:Facebook:ClientId").Value;
                         options.AppSecret = this.Configuration.GetSection("ExternalLogin:Facebook:ClientSecret").Value;
                    });    
            
            services.AddMemoryCache();
            services.AddSession();
            services.AddAutoMapper(typeof(Startup));

        }
      

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider service)
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

            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "categoryFilter", template: "Pizza/{action}/{category?}", defaults: new { controller = "Pizza", action="Index"});
                routes.MapRoute(name: "default", template: "{controller=Home}/{action=Index}/{id?}");
            });

            DbInitializer.Seed(service);
        }
    }
}
