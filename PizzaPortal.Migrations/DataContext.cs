﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PizzaPortal.Model.Models;
using System.Linq;

namespace PizzaPortal.Migrations
{
    public class DataContext : IdentityDbContext
    {
        public DataContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var foreignKey in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Cascade;
            }

            modelBuilder.Entity<Order>().Property(x => x.OrderTotal)
                                        .HasColumnType("decimal(5,2)")
                                        .IsRequired(true);

            modelBuilder.Entity<Pizza>().Property(x => x.Price)
                                       .HasColumnType("decimal(5,2)")
                                       .IsRequired(true);

            modelBuilder.Entity<OrderDetail>().Property(x => x.Price)
                                     .HasColumnType("decimal(5,2)")
                                     .IsRequired(true);
        }

        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<PizzaIngredient> PizzaIngredients { get; set; }
    }
}
