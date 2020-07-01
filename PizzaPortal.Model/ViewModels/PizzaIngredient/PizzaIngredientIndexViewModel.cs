using PizzaPortal.Model.ViewModels.Ingredient;
using PizzaPortal.Model.ViewModels.Pizza;
using System.Collections.Generic;

namespace PizzaPortal.Model.ViewModels.PizzaIngredient
{
    public class PizzaIngredientIndexViewModel : BaseViewModel
    {
        public PizzaIngredientIndexViewModel()
        {
            PageTitle = "PizzaIngredient Index";
            Items = new List<PizzaIngredientItemViewModel>();
        }

        public List<PizzaIngredientItemViewModel> Items { get; set; }
    }

    public class PizzaIngredientItemViewModel
    {
        public string Id { get; set; }
        public string PizzaId { get; set; }
        public string IngredientId { get; set; }

        public virtual PizzaItemViewModel Pizza { get; set; }
        public virtual IngredientItemViewModel Ingredient { get; set; }
    }
}
