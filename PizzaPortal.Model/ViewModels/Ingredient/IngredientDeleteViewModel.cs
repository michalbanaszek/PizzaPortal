namespace PizzaPortal.Model.ViewModels.Ingredient
{
    public class IngredientDeleteViewModel : BaseViewModel
    {
        public IngredientDeleteViewModel()
        {
            PageTitle = "Ingredient Delete";
        }

        public string Id { get; set; }
        public string Name { get; set; }
    }
}
