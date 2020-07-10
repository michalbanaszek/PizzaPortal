using System.ComponentModel;

namespace PizzaPortal.Model.ViewModels.Account
{
    public class ChangePasswordViewModel : BaseViewModel
    {
        public ChangePasswordViewModel()
        {
            PageTitle = "Change Password";
        }

        [DisplayName("Current Password")]
        public string CurrentPassword { get; set; }

        [DisplayName("New Password")]
        public string NewPassword { get; set; }

        [DisplayName("Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}
