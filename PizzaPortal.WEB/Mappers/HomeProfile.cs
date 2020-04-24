using AutoMapper;
using PizzaPortal.Model.Models;
using PizzaPortal.Model.ViewModels.Home;
using PizzaPortal.Model.ViewModels.Pizza;

namespace PizzaPortal.WEB.Mappers
{
    public class HomeProfile : Profile
    {
        public HomeProfile()
        {
            CreateMap<Pizza, ItemPizzaViewModel>();
        }
    }
}
