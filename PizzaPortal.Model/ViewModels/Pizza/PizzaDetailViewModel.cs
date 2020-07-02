namespace PizzaPortal.Model.ViewModels.Pizza
{
    public class PizzaDetailViewModel : BaseViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string PhotoPath { get; set; }
    }
}
