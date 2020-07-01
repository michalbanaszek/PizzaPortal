using AutoMapper;
using PizzaPortal.Model.Models;
using PizzaPortal.Model.ViewModels.Ingredient;

namespace PizzaPortal.WEB.Mappers
{
    public class IngredientProfile : Profile
    {
        public IngredientProfile()
        {
            CreateMap<Ingredient, IngredientItemViewModel>();
            CreateMap<Ingredient, IngredientDetailViewModel>();
            CreateMap<IngredientCreateViewModel, Ingredient>();
            CreateMap<Ingredient, IngredientDeleteViewModel>();
            CreateMap<Ingredient, IngredientEditViewModel>().ReverseMap();
        }
    }
}
