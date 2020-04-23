using System.Collections.Generic;

namespace PizzaPortal.Model.ViewModels.Pizza
{
    public class IndexPizzaViewModel : BaseViewModel
    {
        public IndexPizzaViewModel()
        {
            PageTitle = "Index Pizza";
            Items = new List<ItemPizzaViewModel>();
        }

        public List<ItemPizzaViewModel> Items { get; set; }
        public string CurrentCategory { get; set; }
    }

    public class ItemPizzaViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public bool IsPreferredPizza { get; set; }
        public string CategoryId { get; set; }
    }
}
