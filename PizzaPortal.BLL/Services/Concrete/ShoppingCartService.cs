using PizzaPortal.BLL.Services.Abstract;
using PizzaPortal.DAL.Repositories.Abstract;
using PizzaPortal.Model.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PizzaPortal.BLL.Services.Concrete
{
    public class ShoppingCartService : Service<ShoppingCartItemDTO>, IShoppingCartService
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;

        public ShoppingCartService(IShoppingCartRepository shoppingCartRepository) : base(shoppingCartRepository)
        {
            this._shoppingCartRepository = shoppingCartRepository;
        }

        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItemDTO> ShoppingCartItems { get; set; }


        public async Task<List<ShoppingCartItemDTO>> GetShoppingCartItemsAsync()
        {
            return await this._shoppingCartRepository.GetShoppingCartItemsAsync();
        }

        public async Task AddToCartAsync(PizzaDTO pizza, int amount)
        {
            await _shoppingCartRepository.AddToCartAsync(pizza, amount);
        }

        public async Task ClearCartAsync()
        {
            await this._shoppingCartRepository.ClearCartAsync();
        }

        public async Task<int> RemoveFromCartAsync(PizzaDTO pizza)
        {
            return await this._shoppingCartRepository.RemoveFromCartAsync(pizza);
        }

        public async Task<decimal> GetShoppingCartTotalAsync()
        {
            return await this._shoppingCartRepository.GetShoppingCartTotalAsync();
        }
    }
}
