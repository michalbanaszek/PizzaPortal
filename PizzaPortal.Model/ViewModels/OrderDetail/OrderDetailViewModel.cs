using PizzaPortal.Model.ViewModels.Order;
using PizzaPortal.Model.ViewModels.Pizza;

namespace PizzaPortal.Model.ViewModels.OrderDetail
{
    public class OrderDetailViewModel : BaseViewModel
    {
        public OrderDetailViewModel()
        {
            PageTitle = "Order Detail";
        }

        public string PizzaId { get; set; }
        public string OrderId { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public virtual ItemPizzaViewModel Pizza { get; set; }
        public virtual OrderViewModel Order { get; set; }
    }
}
