namespace PizzaPortal.Model.ViewModels.Ingredient
{
    public class IngredientCreateViewModel : BaseViewModel
    {
        public IngredientCreateViewModel()
        {
            PageTitle = "Ingredient Create";
        }

        public string Name { get; set; }
    }
}
