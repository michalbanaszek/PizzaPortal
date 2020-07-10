using FluentValidation;
using PizzaPortal.Model.ViewModels.Account;

namespace PizzaPortal.WEB.Validators.Account
{
    public class ForgetPasswordValidator : AbstractValidator<ForgotPasswordViewModel>
    {
        public ForgetPasswordValidator()
        {
            RuleFor(x => x.Email).EmailAddress().NotEmpty();
        }
    }
}
