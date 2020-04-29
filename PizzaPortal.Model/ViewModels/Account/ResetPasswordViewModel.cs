namespace PizzaPortal.Model.ViewModels.Account
{
    public class ResetPasswordViewModel : BaseViewModel
    {
        public string Email { get; set; }

        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Token { get; set; }
    }
}
