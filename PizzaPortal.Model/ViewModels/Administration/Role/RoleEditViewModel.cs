using System.Collections.Generic;

namespace PizzaPortal.Model.ViewModels.Administration.Role
{
    public class RoleEditViewModel : BaseViewModel
    {
        public RoleEditViewModel()
        {
            PageTitle = "Edit Role";
            Users = new List<string>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public List<string> Users { get; set; }
    }
}
