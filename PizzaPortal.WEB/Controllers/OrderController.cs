using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PizzaPortal.BLL.Services.Abstract;
using PizzaPortal.Model.Models;
using PizzaPortal.Model.ViewModels.Error;
using PizzaPortal.Model.ViewModels.Order;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PizzaPortal.WEB.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IMapper _mapper;
        private readonly ILogger<OrderController> _logger;
        private readonly UserManager<IdentityUser> _userManager;

        public OrderController(IOrderService orderService,
                               IShoppingCartService shoppingCartService,
                               IMapper mapper,
                               UserManager<IdentityUser> userManager,
                               ILogger<OrderController> logger)
        {
            this._orderService = orderService;
            this._shoppingCartService = shoppingCartService;
            this._mapper = mapper;
            this._userManager = userManager;
            this._logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await this._userManager.GetUserAsync(HttpContext.User);
            var isSuperAdmin = await this._userManager.IsInRoleAsync(user, "Super Admin");
            var isAdmin = await this._userManager.IsInRoleAsync(user, "Admin");
            OrderIndexViewModel viewModel = new OrderIndexViewModel();

            if (isSuperAdmin == true || isAdmin == true)
            {            
                viewModel.Items = this._mapper.Map<List<OrderItemViewModel>>(await this._orderService.GetOrdersAsync());
            }
            else
            {
                viewModel.Items = this._mapper.Map<List<OrderItemViewModel>>(await this._orderService.GetUserOrdersAsync(user.Id));
            }

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            var order = await this._orderService.GetOrderSummaryByIdAsync(id);

            if (order == null)
            {
                var errorViewModel = new NotFoundViewModel()
                {
                    StatusCode = 404,
                    Message = $"Not found this id: {id}"
                };

                return View("NotFound", errorViewModel);
            }

            return View(this._mapper.Map<OrderItemViewModel>(order));
        }

        [HttpGet]
        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(OrderItemViewModel viewModel)
        {
            var userId = this._userManager.GetUserId(HttpContext.User);          

            var cartItems = await this._shoppingCartService.GetShoppingCartItemsAsync();
            this._shoppingCartService.ShoppingCartItems = cartItems;

            if (_shoppingCartService.ShoppingCartItems.Count == 0)
            {
                ModelState.AddModelError(string.Empty, "Cart is empty.");
            }

            if (ModelState.IsValid)
            {
                var orders = this._mapper.Map<Order>(viewModel);
                orders.UserId = userId;

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

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var order = await this._orderService.GetByIdAsync(id);

            if (order == null)
            {
                var errorViewModel = new NotFoundViewModel()
                {
                    StatusCode = 404,
                    Message = $"Not found this id: {id}"
                };

                return View("NotFound", errorViewModel);
            }
         
            return View(this._mapper.Map<OrderItemViewModel>(order));
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(string id)
        {
            var order = await this._orderService.GetByIdAsync(id);

            if (order == null)
            {
                var errorViewModel = new NotFoundViewModel()
                {
                    StatusCode = 404,
                    Message = $"Not found this id: {id}"
                };

                return View("NotFound", errorViewModel);
            }

            try
            {
                var deleted = await this._orderService.DeleteAsync(order.Id);

                if (!deleted)
                {
                    this._logger.LogError($"Cannot delete this order, id: {id}");

                    ViewBag.ErrorMessage = $"Cannot delete this order, id: {id}";

                    return View("Error");
                }

                return RedirectToAction(nameof(Index));

            }
            catch (DbUpdateException ex)
            {
                this._logger.LogError(ex.Message);

                return View("Error");
            }
        }
    }
}