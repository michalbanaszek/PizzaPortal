using PizzaPortal.Model.ViewModels.Ingredient;
using PizzaPortal.Model.ViewModels.Pizza;
using System.ComponentModel;

namespace PizzaPortal.Model.ViewModels.PizzaIngredient
{
    public class PizzaIngredientCreateViewModel : BaseViewModel
    {
        [DisplayName("Select Pizza")]
        public string PizzaId { get; set; }

        [DisplayName("Select Ingredient")]
        public string IngredientId { get; set; }

        public virtual PizzaItemViewModel Pizza { get; set; }
        public virtual IngredientItemViewModel Ingredient { get; set; }
    }
}
