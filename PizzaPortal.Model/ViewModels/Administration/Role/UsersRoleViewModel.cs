using System.Collections.Generic;

namespace PizzaPortal.Model.ViewModels.Administration.Role
{
    public class UsersRoleViewModel : BaseViewModel
    {
        public UsersRoleViewModel()
        {
            PageTitle = "Users Role";
            Items = new List<UserRoleItemViewModel>();
        }

        public List<UserRoleItemViewModel> Items { get; set; }
    }

    public class UserRoleItemViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public bool IsSelected { get; set; }
    }
}
