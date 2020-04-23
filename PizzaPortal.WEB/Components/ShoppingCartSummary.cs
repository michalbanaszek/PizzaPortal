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

            var viewModel = new ShoppingCartViewModel
            {
                Items = this._mapper.Map<List<ItemShoppingCartViewModel>>(items.Result),
                ShoppingCartTotal = _shoppingCartService.GetShoppingCartTotal()
            };

            return View(viewModel);
        }
    }
}
