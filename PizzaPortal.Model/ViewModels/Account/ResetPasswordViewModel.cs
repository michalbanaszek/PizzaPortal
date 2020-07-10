using System.ComponentModel;

namespace PizzaPortal.Model.ViewModels.Account
{
    public class ResetPasswordViewModel : BaseViewModel
    {
        public ResetPasswordViewModel()
        {
            PageTitle = "Reset Password";
        }

        public string Email { get; set; }

        public string Password { get; set; }

        [DisplayName("Confirm Password")]
        public string ConfirmPassword { get; set; }
        public string Token { get; set; }
    }
}
