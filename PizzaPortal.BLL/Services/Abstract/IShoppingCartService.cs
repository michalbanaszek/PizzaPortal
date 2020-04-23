using PizzaPortal.Model.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PizzaPortal.BLL.Services.Abstract
{
    public interface IShoppingCartService : IService<ShoppingCartItemDTO>
    {
        string ShoppingCartId { get; set; }
        List<ShoppingCartItemDTO> ShoppingCartItems { get; set; }

        Task<List<ShoppingCartItemDTO>> GetShoppingCartItemsAsync();
        Task AddToCartAsync(PizzaDTO pizza, int amount);
        Task<int> RemoveFromCartAsync(PizzaDTO pizza);
        Task ClearCartAsync();
        Task<decimal> GetShoppingCartTotalAsync();
    }
}
