using Microsoft.AspNetCore.Http;
using PizzaPortal.Model.ViewModels.Category;
using System.Collections.Generic;

namespace PizzaPortal.Model.ViewModels.Pizza
{
    public class PizzaCreateViewModel : BaseViewModel
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public bool IsPreferredPizza { get; set; }
        public string CategoryId { get; set; }
        public IFormFile Photo { get; set; }

        public List<CategoryItemViewModel> Categories { get; set; }
    }
}
