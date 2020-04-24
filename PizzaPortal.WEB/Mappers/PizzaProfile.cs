using AutoMapper;
using PizzaPortal.Model.Models;
using PizzaPortal.Model.ViewModels.Pizza;

namespace PizzaPortal.WEB.Mappers
{
    public class PizzaProfile : Profile
    {
        public PizzaProfile()
        {
            CreateMap<Pizza, ItemPizzaViewModel>();
            CreateMap<Pizza, DetailsPizzaViewModel>();
            CreateMap<CreatePizzaViewModel, Pizza>();
            CreateMap<Pizza, EditPizzaViewModel>().ReverseMap();
            CreateMap<Pizza, DeletePizzaViewModel>();
        }
    }
}
