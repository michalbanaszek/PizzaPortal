using System.Collections.Generic;

namespace PizzaPortal.Model.ViewModels.Pizza
{
    public class PizzaEditViewModel : PizzaCreateViewModel
    {
        public PizzaEditViewModel()
        {
            PageTitle = "Edit Pizza";
        }

        public string Id { get; set; }
        public string ExistingPhotoPath { get; set; }

        public List<string> Ingredients { get; set; }
    }
}
