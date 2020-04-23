namespace PizzaPortal.Model.ViewModels.Pizza
{
    public class EditPizzaViewModel : CreatePizzaViewModel
    {
        public EditPizzaViewModel()
        {
            PageTitle = "Edit Pizza";
        }

        public string Id { get; set; }
    }
}
