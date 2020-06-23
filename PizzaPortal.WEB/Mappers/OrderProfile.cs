using AutoMapper;
using PizzaPortal.Model.Models;
using PizzaPortal.Model.ViewModels.Order;
using PizzaPortal.Model.ViewModels.OrderDetail;

namespace PizzaPortal.WEB.Mappers
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderItemViewModel>().ReverseMap();
            CreateMap<OrderDetail, OrderDetailViewModel>();
        }
    }
}
