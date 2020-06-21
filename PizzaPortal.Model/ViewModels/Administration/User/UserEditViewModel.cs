using System.Collections.Generic;

namespace PizzaPortal.Model.ViewModels.Administration.User
{
    public class UserEditViewModel : BaseViewModel
    {
        public UserEditViewModel()
        {
            PageTitle = "Edit Role";
            Roles = new List<string>();
            Claims = new List<string>();
        }

        public string Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public IList<string> Roles { get; set; }
        public List<string> Claims { get; set; }
    }
}
