using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PizzaPortal.BLL.Services.Abstract;
using PizzaPortal.Model.ViewModels.ShoppingCart;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PizzaPortal.WEB.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IPizzaService _pizzaService;
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IMapper _mapper;

        public ShoppingCartController(IPizzaService pizzaService, IShoppingCartService shoppingCartService, IMapper mapper)
        {
            this._pizzaService = pizzaService;
            this._shoppingCartService = shoppingCartService;
            this._mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var items = await _shoppingCartService.GetShoppingCartItemsAsync();

            _shoppingCartService.ShoppingCartItems = items;

            var shoppingCartViewModel = new ShoppingCartViewModel
            {
                Items = this._mapper.Map<List<ItemShoppingCartViewModel>>(items),
                ShoppingCartTotal = await _shoppingCartService.GetShoppingCartTotalAsync()
            };
            return View(shoppingCartViewModel);
        }

        public async Task<IActionResult> AddToShoppingCart(string pizzaId)
        {
            var selectedPizza = await this._pizzaService.GetByIdAsync(pizzaId);

            if (selectedPizza != null)
            {
                await this._shoppingCartService.AddToCartAsync(selectedPizza, 1);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> RemoveFromShoppingCart(string pizzaId)
        {
            var selectedPizza = await this._pizzaService.GetByIdAsync(pizzaId);

            if (selectedPizza != null)
            {
                await this._shoppingCartService.RemoveFromCartAsync(selectedPizza);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ClearCart()
        {
            await this._shoppingCartService.ClearCartAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}