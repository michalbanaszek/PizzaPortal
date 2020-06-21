using System.Collections.Generic;

namespace PizzaPortal.Model.ViewModels.Administration.User
{
    public class UsersViewModel : BaseViewModel
    {
        public UsersViewModel()
        {
            PageTitle = "Users List";
        }

        public List<UserItemViewModel> Items { get; set; }
    }

    public class UserItemViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}
