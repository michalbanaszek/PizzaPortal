using PizzaPortal.Model.Models;
using PizzaPortal.Model.ViewModels.Pizza;
using System.Collections.Generic;

namespace PizzaPortal.Model.ViewModels.ShoppingCart
{
    public class ShoppingCartViewModel : BaseViewModel
    {
        public ShoppingCartViewModel()
        {
            PageTitle = "Index Cart";
            Items = new List<ItemShoppingCartViewModel>();
        }

        public List<ItemShoppingCartViewModel> Items { get; set; }
        public decimal ShoppingCartTotal { get; set; }
    }

    public class ItemShoppingCartViewModel
    {
        public PizzaItemViewModel Pizza { get; set; }
        public int Amount { get; set; }
        public string ShoppingCartId { get; set; }
    }
}
