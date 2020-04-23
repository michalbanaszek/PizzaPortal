using PizzaPortal.Model.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PizzaPortal.DAL.Repositories.Abstract
{
    public interface IShoppingCartRepository : IRepository<ShoppingCartItemDTO>
    {
        string ShoppingCartId { get; set; }
        List<ShoppingCartItemDTO> ShoppingCartItems { get; set; }

        Task<List<ShoppingCartItemDTO>> GetShoppingCartItemsAsync();
        Task AddToCartAsync(PizzaDTO pizza, int amount);
        Task<int> RemoveFromCartAsync(PizzaDTO pizza);
        Task ClearCartAsync();
        decimal GetShoppingCartTotal();
    }
}
