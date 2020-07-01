using PizzaPortal.Model.ViewModels.Ingredient;
using PizzaPortal.Model.ViewModels.Pizza;

namespace PizzaPortal.Model.ViewModels.PizzaIngredient
{
    public class PizzaIngredientDetailViewModel : BaseViewModel
    {
        public string Id { get; set; }
        public string PizzaId { get; set; }
        public string IngredientId { get; set; }

        public virtual PizzaItemViewModel Pizza { get; set; }
        public virtual IngredientItemViewModel Ingredient { get; set; }
    }
}
