using FluentValidation;
using PizzaPortal.Model.ViewModels.Account;

namespace PizzaPortal.WEB.Validators.Account
{
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordViewModel>
    {
        public ChangePasswordValidator()
        {
            RuleFor(x => x.CurrentPassword)
                .NotEmpty();

            RuleFor(x => x.NewPassword)
                .NotEmpty();

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.NewPassword)
                .WithMessage("Passwords do not match")
                .NotEmpty();
        }
    }
}
