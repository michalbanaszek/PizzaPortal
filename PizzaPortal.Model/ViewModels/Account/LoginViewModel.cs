using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaPortal.Model.ViewModels.Account
{
    public class LoginViewModel : BaseViewModel
    {
        public LoginViewModel()
        {
            PageTitle = "Login";
        }

        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public string ReturnUrl { get; set; }
    }
}
