namespace PizzaPortal.Model.ViewModels.Administration.User
{
    public class UserRolesViewModel : BaseViewModel
    {
        public UserRolesViewModel()
        {
            PageTitle = "User Roles";
        }

        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public bool IsSelected { get; set; }
    }
}
