using PizzaPortal.Model.ViewModels.Category;
using System.Collections.Generic;

namespace PizzaPortal.Model.ViewModels.Pizza
{
    public class PizzaIndexViewModel : BaseViewModel
    {
        public PizzaIndexViewModel()
        {
            PageTitle = "Index Pizza";
            Items = new List<PizzaItemViewModel>();
        }

        public List<PizzaItemViewModel> Items { get; set; }
        public string CurrentCategory { get; set; }
    }

    public class PizzaItemViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public bool IsPreferredPizza { get; set; }
        public string PhotoPath { get; set; }
        public string CategoryId { get; set; }
        public CategoryItemViewModel Category { get; set; }
    }
}
