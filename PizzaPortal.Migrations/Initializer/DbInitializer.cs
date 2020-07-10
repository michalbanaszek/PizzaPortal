using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using PizzaPortal.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PizzaPortal.Migrations.Initializer
{
    public class DbInitializer
    {
        public static void Initialize(IServiceProvider service)
        {
            DataContext context = service.GetRequiredService<DataContext>();

            context.Database.EnsureCreated();

            if (context.Pizzas.Any())
            {
                return;
            }

            var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = service.GetRequiredService<UserManager<IdentityUser>>();

            ClearDatabase(context);
            CreateAdminRole(context, roleManager, userManager);
            SeedDatabase(context, roleManager, userManager);
        }

        private static void CreateAdminRole(DataContext context, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            var superAdminRole = new IdentityRole()
            {
                Name = "Super Admin"
            };

            var adminRole = new IdentityRole()
            {
                Name = "Admin"
            };

            roleManager.CreateAsync(superAdminRole).Wait();
            roleManager.CreateAsync(adminRole).Wait();

            var superAdminUser = new IdentityUser { UserName = "superadmin@gmail.com", Email = "superadmin@gmail.com", EmailConfirmed = true };
            var AdminUser = new IdentityUser { UserName = "admin@gmail.com", Email = "admin@gmail.com", EmailConfirmed = true };

            string adminPassword = "Qwerty!1";

            var adminUsers = new List<IdentityUser>()
            {
                superAdminUser, AdminUser
            };

            IdentityResult result = null;

            foreach (var admin in adminUsers)
            {
                result = userManager.CreateAsync(admin, adminPassword).Result;
            }

            if (result.Succeeded)
            {
                userManager.AddToRoleAsync(superAdminUser, "Super Admin").Wait();
            }

            if (result.Succeeded)
            {
                userManager.AddToRoleAsync(AdminUser, "Admin").Wait();
            }
        }

        private static void SeedDatabase(DataContext context, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            var category1 = new Category { Name = "Normal" };
            var category2 = new Category { Name = "Vega" };

            var categories = new List<Category>()
            {
                category1, category2
            };

            var pizza1 = new Pizza
            {
                Name = "Pizza 1",
                Price = 7.95M,
                Description = "Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled ",
                IsPreferredPizza = true,
                PhotoPath = "pizza1.png",
                Category = category2,
            };
            var pizza2 = new Pizza
            {
                Name = "Pizza 2",
                Price = 9.95M,
                Description = "Lorem Ipsum has been the industry's standard dummy text ever since the 1600s, when an unknown printer took a galley of type and scrambled ",
                IsPreferredPizza = true,
                PhotoPath = "pizza2.png",
                Category = category1,
            };
            var pizza3 = new Pizza
            {
                Name = "Pizza 3",
                Price = 8.95M,
                Description = "Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled ",
                IsPreferredPizza = true,
                PhotoPath = "pizza3.png",
                Category = category1,
            };
            var pizza4 = new Pizza
            {
                Name = "Pizza 4",
                Price = 4.95M,
                Description = "Lorem Ipsum has been the industry's standard dummy text ever since the 1600s, when an unknown printer took a galley of type and scrambled ",
                IsPreferredPizza = false,
                PhotoPath = "pizza4.png",
                Category = category1,
            };
            var pizza5 = new Pizza
            {
                Name = "Pizza 5",
                Price = 6.95M,
                Description = "Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled ",
                IsPreferredPizza = false,
                PhotoPath = "pizza5.png",
                Category = category1,
            };
            var pizza6 = new Pizza
            {
                Name = "Pizza 6",
                Price = 3.95M,
                Description = "Lorem Ipsum has been the industry's standard dummy text ever since the 1600s, when an unknown printer took a galley of type and scrambled ",
                IsPreferredPizza = false,
                PhotoPath = "pizza6.png",
                Category = category2,
            };

            var pizzas = new List<Pizza>()
            {
                pizza1, pizza2, pizza3, pizza4, pizza5, pizza6
            };

            var user1 = new IdentityUser { UserName = "user1@gmail.com", Email = "user1@gmail.com", EmailConfirmed = true };
            var user2 = new IdentityUser { UserName = "user2@gmail.com", Email = "user2@gmail.com", EmailConfirmed = true };

            string userPassword = "Qwerty!1";

            var users = new List<IdentityUser>()
            {
                user1, user2
            };

            foreach (var user in users)
            {
                userManager.CreateAsync(user, userPassword).Wait();
            }

            var ing1 = new Ingredient { Name = "Cheese" };
            var ing2 = new Ingredient { Name = "Ham" };
            var ing3 = new Ingredient { Name = "Mushrooms" };
            var ing4 = new Ingredient { Name = "Pineapple" };
            var ing5 = new Ingredient { Name = "Olives" };
            var ing6 = new Ingredient { Name = "Kebab" };
            var ing7 = new Ingredient { Name = "Tomatoes" };
            var ing8 = new Ingredient { Name = "Onion" };
            var ing9 = new Ingredient { Name = "Chicken" };
            var ing10 = new Ingredient { Name = "Salami" };

            var ingredients = new List<Ingredient>()
            {
                ing1, ing2, ing3, ing4, ing5, ing6, ing7, ing8, ing9, ing10
            };

            var pizzaIngs = new List<PizzaIngredient>()
            {
                new PizzaIngredient { Ingredient = ing1, Pizza = pizza1 },
                new PizzaIngredient { Ingredient = ing2, Pizza = pizza1 },
                new PizzaIngredient { Ingredient = ing3, Pizza = pizza1 },
                new PizzaIngredient { Ingredient = ing5, Pizza = pizza1 },
                new PizzaIngredient { Ingredient = ing9, Pizza = pizza1 },

                new PizzaIngredient { Ingredient = ing1, Pizza = pizza2 },
                new PizzaIngredient { Ingredient = ing2, Pizza = pizza2 },
                new PizzaIngredient { Ingredient = ing3, Pizza = pizza2 },
                new PizzaIngredient { Ingredient = ing4, Pizza = pizza2 },
                new PizzaIngredient { Ingredient = ing10, Pizza = pizza2 },

                new PizzaIngredient { Ingredient = ing1, Pizza = pizza3 },
                new PizzaIngredient { Ingredient = ing2, Pizza = pizza3 },
                new PizzaIngredient { Ingredient = ing3, Pizza = pizza3 },
                new PizzaIngredient { Ingredient = ing8, Pizza = pizza3 },
                new PizzaIngredient { Ingredient = ing9, Pizza = pizza3 },

                new PizzaIngredient { Ingredient = ing1, Pizza = pizza4 },
                new PizzaIngredient { Ingredient = ing2, Pizza = pizza4 },
                new PizzaIngredient { Ingredient = ing3, Pizza = pizza4 },

                new PizzaIngredient { Ingredient = ing1, Pizza = pizza5 },
                new PizzaIngredient { Ingredient = ing2, Pizza = pizza5 },
                new PizzaIngredient { Ingredient = ing3, Pizza = pizza5 },
                new PizzaIngredient { Ingredient = ing6, Pizza = pizza5 },
                new PizzaIngredient { Ingredient = ing4, Pizza = pizza5 },
                new PizzaIngredient { Ingredient = ing7, Pizza = pizza5 },

                new PizzaIngredient { Ingredient = ing1, Pizza = pizza6 },
                new PizzaIngredient { Ingredient = ing2, Pizza = pizza6 },
                new PizzaIngredient { Ingredient = ing3, Pizza = pizza6 },
                new PizzaIngredient { Ingredient = ing4, Pizza = pizza6 },
                new PizzaIngredient { Ingredient = ing7, Pizza = pizza6 }
            };

            var ord1 = new Order
            {
                FirstName = "Tomasz",
                LastName = "Kowalski",
                AddressLine1 = "Kolorowa 12",
                Email = "t.kowalski@gmail.com",
                OrderPlaced = DateTime.Now.AddDays(-1),
                PhoneNumber = "00110011",
                User = user1,
                ZipCode = "43210",
                OrderTotal = 75.00M,
            };

            var orderDetails = new List<OrderDetail>()
            {
                new OrderDetail { Order = ord1, Pizza = pizza1, Amount = 2, Price = pizza1.Price},
                new OrderDetail { Order = ord1, Pizza = pizza3, Amount = 1, Price = pizza3.Price},
                new OrderDetail { Order = ord1, Pizza = pizza5, Amount = 3, Price = pizza5.Price},
            };

            var orders = new List<Order>()
            {
                ord1
            };

            context.Categories.AddRange(categories);
            context.Pizzas.AddRange(pizzas);
            context.Orders.AddRange(orders);
            context.OrderDetails.AddRange(orderDetails);
            context.Ingredients.AddRange(ingredients);
            context.PizzaIngredients.AddRange(pizzaIngs);

            context.SaveChanges();
        }

        public static void ClearDatabase(DataContext context)
        {
            var pizzaIngredients = context.PizzaIngredients.ToList();
            context.PizzaIngredients.RemoveRange(pizzaIngredients);

            var ingredients = context.Ingredients.ToList();
            context.Ingredients.RemoveRange(ingredients);

            var shoppingCartItems = context.ShoppingCartItems.ToList();
            context.ShoppingCartItems.RemoveRange(shoppingCartItems);

            var users = context.Users.ToList();
            var userRoles = context.UserRoles.ToList();

            foreach (var user in users)
            {
                if (!userRoles.Any(r => r.UserId == user.Id))
                {
                    context.Users.Remove(user);
                }
            }

            var orderDetails = context.OrderDetails.ToList();
            context.OrderDetails.RemoveRange(orderDetails);

            var orders = context.Orders.ToList();
            context.Orders.RemoveRange(orders);

            var pizzas = context.Pizzas.ToList();
            context.Pizzas.RemoveRange(pizzas);

            var categories = context.Categories.ToList();
            context.Categories.RemoveRange(categories);

            context.SaveChanges();
        }
    }
}
