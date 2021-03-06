﻿using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PizzaPortal.DAL.Repositories.Abstract;
using PizzaPortal.Migrations;
using PizzaPortal.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaPortal.DAL.Repositories.Concrete
{
    public class ShoppingCartRepository : Repository<ShoppingCartItem>, IShoppingCartRepository
    {
        private readonly DataContext _context;

        public ShoppingCartRepository(DataContext context) : base(context)
        {
            this._context = context;
        }

        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        public static ShoppingCartRepository GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()
                                       .HttpContext.Session;

            var context = services.GetService<DataContext>();

            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();

            session.SetString("CartId", cartId);

            return new ShoppingCartRepository(context) { ShoppingCartId = cartId };
        }

        public async Task<List<ShoppingCartItem>> GetShoppingCartItemsAsync()
        {
            return ShoppingCartItems ?? await this._context.ShoppingCartItems
                                .Where(x => x.ShoppingCartId == ShoppingCartId)
                                .Include(x => x.Pizza)
                                .ToListAsync();
        }

        public async Task AddToCartAsync(Pizza pizza, int amount)
        {
            var shoppingCartItem = await this._context.ShoppingCartItems
                                             .SingleOrDefaultAsync(x => x.Pizza.Id == pizza.Id &&
                                                                   x.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = ShoppingCartId,
                    Pizza = pizza,
                    Amount = 1
                };

                await base.CreateAsync(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }

            await this._context.SaveChangesAsync();
        }

        public async Task RemoveFromCartAsync(Pizza pizza)
        {
            var shoppingCartItem = await this._context.ShoppingCartItems
                                             .SingleOrDefaultAsync(x => x.Pizza.Id == pizza.Id &&
                                                                   x.ShoppingCartId == ShoppingCartId);       

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;

                    await base.UpdateAsync(shoppingCartItem);                 
                }
                else
                {
                    await base.DeleteAsync(shoppingCartItem.Id);
                }
            }
        }

        public async Task ClearCartAsync()
        {
            var cartItems = this._context.ShoppingCartItems.Where(cart => cart.ShoppingCartId == ShoppingCartId);

            this._context.ShoppingCartItems.RemoveRange(cartItems);

            await this._context.SaveChangesAsync();
        }

        public async Task<decimal> GetShoppingCartTotalAsync()
        {
            var total = await _context.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                .Select(c => c.Pizza.Price * c.Amount).SumAsync();
            return total;
        }

        public async Task RemoveItemFromCartAsync(string pizzaId)
        {
            var shoppingCartItem = await this._context.ShoppingCartItems
                                    .SingleOrDefaultAsync(x => x.Pizza.Id == pizzaId &&
                                                          x.ShoppingCartId == ShoppingCartId);

            await base.DeleteAsync(shoppingCartItem.Id);
        }
    }
}
