﻿using Microsoft.AspNetCore.Authentication;
using System.Collections.Generic;
using System.ComponentModel;

namespace PizzaPortal.Model.ViewModels.Account
{
    public class LoginViewModel : BaseViewModel
    {
        public LoginViewModel()
        {
            PageTitle = "Login";
            ExternalLogins = new List<AuthenticationScheme>();
        }

        public string Email { get; set; }
        public string Password { get; set; }

        [DisplayName("Remember Me")]
        public bool RememberMe { get; set; }
        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }
    }
}
