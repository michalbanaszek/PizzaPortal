using AutoMapper;
using PizzaPortal.Model.Models;
using PizzaPortal.Model.ViewModels.Pizza;

namespace PizzaPortal.WEB.Mappers
{
    public class PizzaProfile : Profile
    {
        public PizzaProfile()
        {
            CreateMap<Pizza, PizzaItemViewModel>();
            CreateMap<Pizza, PizzaDetailViewModel>();
            CreateMap<PizzaCreateViewModel, Pizza>();
            CreateMap<Pizza, PizzaEditViewModel>().ForMember(dest => dest.ExistingPhotoPath, opt => opt.MapFrom(src => src.PhotoPath)).ReverseMap();
            CreateMap<Pizza, PizzaDeleteViewModel>();
        }
    }
}
