using FluentValidation;
using PizzaPortal.Model.ViewModels.Account;

namespace PizzaPortal.WEB.Validators.Account
{
    public class LoginValidator : AbstractValidator<LoginViewModel>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Email).EmailAddress().NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
