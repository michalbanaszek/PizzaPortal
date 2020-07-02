using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PizzaPortal.BLL.Services.Abstract;
using PizzaPortal.Model.ViewModels.ShoppingCart;
using System.Collections.Generic;

namespace PizzaPortal.WEB.Components
{
    public class ShoppingCartSummary : ViewComponent
    {
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IMapper _mapper;

        public ShoppingCartSummary(IShoppingCartService shoppingCartService, IMapper mapper)
        {
            this._shoppingCartService = shoppingCartService;
            this._mapper = mapper;
        }

        public IViewComponentResult Invoke()
        {
            var items = this._shoppingCartService.GetShoppingCartItemsAsync();

            this._shoppingCartService.ShoppingCartItems = items.Result;

            var viewModel = new ShoppingCartIndexViewModel
            {
                Items = this._mapper.Map<List<ShoppingCartItemViewModel>>(items.Result),
                ShoppingCartTotal = _shoppingCartService.GetShoppingCartTotalAsync().Result
            };

            return View(viewModel);
        }
    }
}
