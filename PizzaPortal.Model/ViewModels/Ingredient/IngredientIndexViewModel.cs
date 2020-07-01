using System.Collections.Generic;

namespace PizzaPortal.Model.ViewModels.Ingredient
{
    public class IngredientIndexViewModel : BaseViewModel
    {
        public IngredientIndexViewModel()
        {
            PageTitle = "Ingredient Index";

            Items = new List<IngredientItemViewModel>();
        }

        public List<IngredientItemViewModel> Items { get; set; }
    }

    public class IngredientItemViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
