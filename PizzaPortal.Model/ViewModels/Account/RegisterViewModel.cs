namespace PizzaPortal.Model.ViewModels.Account
{
    public class RegisterViewModel : BaseViewModel
    {
        public RegisterViewModel()
        {
            PageTitle = "Register";
        }

        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
