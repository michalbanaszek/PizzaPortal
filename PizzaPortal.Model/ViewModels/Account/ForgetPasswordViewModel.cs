namespace PizzaPortal.Model.ViewModels.Account
{
    public class ForgotPasswordViewModel : BaseViewModel
    {
        public ForgotPasswordViewModel()
        {
            PageTitle = "Forgot Password";
        }

        public string Email { get; set; }
    }
}
