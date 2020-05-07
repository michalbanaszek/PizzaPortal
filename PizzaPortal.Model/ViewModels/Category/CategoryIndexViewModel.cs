using System.Collections.Generic;

namespace PizzaPortal.Model.ViewModels.Category
{
    public class CategoryIndexViewModel : BaseViewModel
    {
        public CategoryIndexViewModel()
        {
            PageTitle = "Index Category";
            Items = new List<CategoryItemViewModel>();
        }

        public List<CategoryItemViewModel> Items { get; set; }
    }

    public class CategoryItemViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
