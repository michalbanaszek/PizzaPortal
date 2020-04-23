using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PizzaPortal.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PizzaPortal.Migrations.Initializer
{
    public class DbInitializer
    {
        public static void Seed(IServiceProvider service)
        {
            DataContext context = service.GetRequiredService<DataContext>();

            if (!context.Categories.Any())
            {
                context.Categories.AddRange(Categories.Select(c => c.Value));
            }

            if (!context.Pizzas.Any())
            {
                context.AddRange(
                     new PizzaDTO
                     {
                         Name = "Pizza 1",
                         Price = 7.95M,
                         Description = "Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled ",
                         IsPreferredPizza = true,
                         Category = Categories["Vega"],
                     },
                     new PizzaDTO
                     {
                         Name = "Pizza 2",
                         Price = 9.95M,
                         Description = "Lorem Ipsum has been the industry's standard dummy text ever since the 1600s, when an unknown printer took a galley of type and scrambled ",
                         IsPreferredPizza = true,
                         Category = Categories["Normal"],
                     },
                     new PizzaDTO
                     {
                         Name = "Pizza 3",
                         Price = 8.95M,
                         Description = "Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled ",
                         IsPreferredPizza = true,
                         Category = Categories["Vega"],
                     },
                     new PizzaDTO
                     {
                         Name = "Pizza 4",
                         Price = 4.95M,
                         Description = "Lorem Ipsum has been the industry's standard dummy text ever since the 1600s, when an unknown printer took a galley of type and scrambled ",
                         IsPreferredPizza = false,
                         Category = Categories["Normal"],
                     },
                     new PizzaDTO
                     {
                         Name = "Pizza 5",
                         Price = 6.95M,
                         Description = "Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled ",
                         IsPreferredPizza = false,
                         Category = Categories["Vega"],
                     },
                     new PizzaDTO
                     {
                         Name = "Pizza 6",
                         Price = 3.95M,
                         Description = "Lorem Ipsum has been the industry's standard dummy text ever since the 1600s, when an unknown printer took a galley of type and scrambled ",
                         IsPreferredPizza = false,
                         Category = Categories["Vega"],
                     });

                context.SaveChanges();
            }
        }

        private static Dictionary<string, CategoryDTO> categories;

        public static Dictionary<string, CategoryDTO> Categories
        {
            get
            {
                if (categories == null)
                {
                    var categoryList = new CategoryDTO[]
                    {
                        new CategoryDTO { Name = "Normal", },
                        new CategoryDTO { Name = "Vega" }
                    };

                    categories = new Dictionary<string, CategoryDTO>();

                    foreach (CategoryDTO cat in categoryList)
                    {
                        categories.Add(cat.Name, cat);
                    }
                }

                return categories;
            }
        }
    }
}
