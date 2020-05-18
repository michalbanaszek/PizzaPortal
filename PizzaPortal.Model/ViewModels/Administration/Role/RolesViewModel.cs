using System.Collections.Generic;

namespace PizzaPortal.Model.ViewModels.Administration.Role
{
    public class RolesViewModel : BaseViewModel
    {
        public RolesViewModel()
        {
            PageTitle = "Roles";
            Items = new List<RoleItemViewModel>();
        }

        public List<RoleItemViewModel> Items { get; set; }
    }

    public class RoleItemViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
