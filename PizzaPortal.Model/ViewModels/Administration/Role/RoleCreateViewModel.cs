namespace PizzaPortal.Model.ViewModels.Administration.Role
{
    public class RoleCreateViewModel : BaseViewModel
    {
        public RoleCreateViewModel()
        {
            PageTitle = "Create Role";
        }

        public string Name { get; set; }
    }
}
