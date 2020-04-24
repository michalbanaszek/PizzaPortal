using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PizzaPortal.BLL.Services.Abstract;
using PizzaPortal.Model.Models;
using PizzaPortal.Model.ViewModels.Order;
using System.Threading.Tasks;

namespace PizzaPortal.WEB.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IMapper _mapper;

        public OrderController(IOrderService orderService, IShoppingCartService shoppingCartService, IMapper mapper)
        {
            this._orderService = orderService;
            this._shoppingCartService = shoppingCartService;
            this._mapper = mapper;
        }

        [HttpGet]
        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(OrderViewModel viewModel)
        {
            var cartItems = await this._shoppingCartService.GetShoppingCartItemsAsync();
            this._shoppingCartService.ShoppingCartItems = cartItems;

            if (_shoppingCartService.ShoppingCartItems.Count == 0)
            {
                ModelState.AddModelError(string.Empty, "Cart is empty.");
            }

            if (ModelState.IsValid)
            {
                var orders = this._mapper.Map<Order>(viewModel);
                await this._orderService.NewOrderAsync(orders);

                await this._shoppingCartService.ClearCartAsync();

                return RedirectToAction("CheckoutComplete");
            }

            return View(viewModel);
        }

        public IActionResult CheckoutComplete()
        {
            return View();
        }
    }
}