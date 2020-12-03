using FluentValidation;
using PizzaPortal.Model.ViewModels.Account;

namespace PizzaPortal.WEB.Validators.Account
{
    public class ResetPasswordValidator : AbstractValidator<ResetPasswordViewModel>
    {
        public ResetPasswordValidator()
        {
            RuleFor(x => x.Password)
                .NotEmpty();

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password)
                .WithMessage("Passwords do not match")
                .NotEmpty();
        }
    }
}
