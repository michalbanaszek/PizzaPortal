namespace PizzaPortal.Model.ViewModels.Pizza
{
    public class DeletePizzaViewModel : BaseViewModel
    {
        public DeletePizzaViewModel()
        {
            PageTitle = "Delete Pizza";
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
