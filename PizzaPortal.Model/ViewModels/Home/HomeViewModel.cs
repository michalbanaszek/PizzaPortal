using PizzaPortal.Model.Models;
using PizzaPortal.Model.ViewModels.Pizza;
using System.Collections.Generic;

namespace PizzaPortal.Model.ViewModels.Home
{
    public class HomeViewModel : BaseViewModel
    {
        public IEnumerable<PizzaItemViewModel> PrefferedPizza { get; set; }
    }
}
