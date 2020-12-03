using FluentValidation;
using PizzaPortal.Model.ViewModels.Account;

namespace PizzaPortal.WEB.Validators.Account
{
    public class RegisterValidator : AbstractValidator<RegisterViewModel>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.Email)
                .EmailAddress()
                .NotEmpty();

            RuleFor(x => x.Password)
                .NotEmpty();

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password)
                .WithMessage("Passwords do not match")
                .NotEmpty();
        }
    }
}
