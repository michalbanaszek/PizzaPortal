using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaPortal.Model.ViewModels.Category
{
    public class IndexCategoryViewModel : BaseViewModel
    {
        public IndexCategoryViewModel()
        {
            PageTitle = "Index Category";
            Items = new List<ItemCategoryViewModel>();
        }

        public List<ItemCategoryViewModel> Items { get; set; }
    }

    public class ItemCategoryViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
