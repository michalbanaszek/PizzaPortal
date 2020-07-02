using PizzaPortal.Model.ViewModels.Order;
using PizzaPortal.Model.ViewModels.Pizza;
using System.Collections.Generic;

namespace PizzaPortal.Model.ViewModels.OrderDetail
{
    public class OrderDetailIndexViewModel : BaseViewModel
    {
        public OrderDetailIndexViewModel()
        {
            PageTitle = "Order Detail";
            Items = new List<OrderDetailItemViewModel>();
        }

        public List<OrderDetailItemViewModel> Items { get; set; }
    }

    public class OrderDetailItemViewModel
    {
        public string PizzaId { get; set; }
        public string OrderId { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public virtual PizzaItemViewModel Pizza { get; set; }
        public virtual OrderItemViewModel Order { get; set; }
    }
}
