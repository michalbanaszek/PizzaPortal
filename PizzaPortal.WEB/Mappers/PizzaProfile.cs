using AutoMapper;
using PizzaPortal.Model.Models;
using PizzaPortal.Model.ViewModels.Pizza;

namespace PizzaPortal.WEB.Mappers
{
    public class PizzaProfile : Profile
    {
        public PizzaProfile()
        {
            CreateMap<PizzaDTO, ItemPizzaViewModel>();
            CreateMap<PizzaDTO, DetailsPizzaViewModel>();
            CreateMap<CreatePizzaViewModel, PizzaDTO>();
            CreateMap<PizzaDTO, EditPizzaViewModel>().ReverseMap();
            CreateMap<PizzaDTO, DeletePizzaViewModel>();
        }
    }
}
