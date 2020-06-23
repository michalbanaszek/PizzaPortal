using PizzaPortal.Model.ViewModels.OrderDetail;
using System;
using System.Collections.Generic;

namespace PizzaPortal.Model.ViewModels.Order
{
    public class OrderViewModel : BaseViewModel
    {
        public OrderViewModel()
        {
            PageTitle = "Checkout";
        }

        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public List<OrderDetailViewModel> OrderDetails { get; set; }

        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string ZipCode { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public decimal OrderTotal { get; set; }
        public DateTime OrderPlaced { get; set; }
        public string UserId { get; set; }
    }
}
