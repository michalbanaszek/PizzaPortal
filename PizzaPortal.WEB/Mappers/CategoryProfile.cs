using AutoMapper;
using PizzaPortal.Model.Models;
using PizzaPortal.Model.ViewModels.Category;

namespace PizzaPortal.WEB.Mappers
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryItemViewModel>();
            CreateMap<CategoryCreateViewModel, Category>();
            CreateMap<Category, CategoryEditViewModel>().ReverseMap();
            CreateMap<Category, CategoryDetailsViewModel>();
            CreateMap<Category, CategoryDeleteViewModel>();
        }
    }
}
