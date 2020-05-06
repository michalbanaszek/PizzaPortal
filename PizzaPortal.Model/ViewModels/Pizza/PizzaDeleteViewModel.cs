namespace PizzaPortal.Model.ViewModels.Pizza
{
    public class PizzaDeleteViewModel : BaseViewModel
    {
        public PizzaDeleteViewModel()
        {
            PageTitle = "Delete Pizza";
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public bool IsPreferredPizza { get; set; }
        public string CategoryId { get; set; }
        public string PhotoPath { get; set; }
    }
}
