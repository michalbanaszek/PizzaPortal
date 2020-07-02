using PizzaPortal.Model.ViewModels.Pizza;
using System.Collections.Generic;

namespace PizzaPortal.Model.ViewModels.ShoppingCart
{
    public class ShoppingCartIndexViewModel : BaseViewModel
    {
        public ShoppingCartIndexViewModel()
        {
            PageTitle = "Index Cart";
            Items = new List<ShoppingCartItemViewModel>();
        }

        public List<ShoppingCartItemViewModel> Items { get; set; }
        public decimal ShoppingCartTotal { get; set; }
    }

    public class ShoppingCartItemViewModel
    {
        public PizzaItemViewModel Pizza { get; set; }
        public int Amount { get; set; }
        public string ShoppingCartId { get; set; }
    }
}
