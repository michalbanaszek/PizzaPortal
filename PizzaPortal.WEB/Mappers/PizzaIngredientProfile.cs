using AutoMapper;
using PizzaPortal.Model.Models;
using PizzaPortal.Model.ViewModels.PizzaIngredient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaPortal.WEB.Mappers
{
    public class PizzaIngredientProfile : Profile
    {
        public PizzaIngredientProfile()
        {
            CreateMap<PizzaIngredient, PizzaIngredientItemViewModel>();
            CreateMap<PizzaIngredient, PizzaIngredientDetailViewModel>();
            CreateMap<PizzaIngredient, PizzaIngredientEditViewModel>().ReverseMap();
            CreateMap<PizzaIngredientCreateViewModel, PizzaIngredient>();
            CreateMap<PizzaIngredient, PizzaIngredientDeleteViewModel>();
        }
    }
}
