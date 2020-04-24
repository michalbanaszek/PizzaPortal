using PizzaPortal.Model.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PizzaPortal.DAL.Repositories.Abstract
{
    public interface IShoppingCartRepository : IRepository<ShoppingCartItem>
    {
        string ShoppingCartId { get; set; }
        List<ShoppingCartItem> ShoppingCartItems { get; set; }

        Task<List<ShoppingCartItem>> GetShoppingCartItemsAsync();
        Task AddToCartAsync(Pizza pizza, int amount);
        Task<int> RemoveFromCartAsync(Pizza pizza);
        Task ClearCartAsync();
        Task<decimal> GetShoppingCartTotalAsync();
    }
}
