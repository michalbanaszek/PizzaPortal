using AutoMapper;
using PizzaPortal.Model.Models;
using PizzaPortal.Model.ViewModels.ShoppingCart;

namespace PizzaPortal.WEB.Mappers
{
    public class ShoppingCartProfile : Profile
    {
        public ShoppingCartProfile()
        {
            CreateMap<ShoppingCartItem, ShoppingCartItemViewModel>();
        }
    }
}
