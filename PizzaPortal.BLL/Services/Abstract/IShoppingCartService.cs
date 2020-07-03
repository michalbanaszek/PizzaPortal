using PizzaPortal.Model.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PizzaPortal.BLL.Services.Abstract
{
    public interface IShoppingCartService : IService<ShoppingCartItem>
    {
        string ShoppingCartId { get; set; }
        List<ShoppingCartItem> ShoppingCartItems { get; set; }

        Task<List<ShoppingCartItem>> GetShoppingCartItemsAsync();
        Task AddToCartAsync(Pizza pizza, int amount);
        Task RemoveFromCartAsync(Pizza pizza);
        Task RemoveItemFromCartAsync(string pizzaId);
        Task ClearCartAsync();
        Task<decimal> GetShoppingCartTotalAsync();
    }
}
