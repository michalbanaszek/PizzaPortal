using FluentValidation;
using PizzaPortal.Model.ViewModels.Order;

namespace PizzaPortal.WEB.Validators.Order
{
    public class OrderCreateValidator : AbstractValidator<OrderCreateViewModel>
    {
        public OrderCreateValidator()
        {
            RuleFor(x => x.FirstName)
                .Length(3, 20)
                .NotEmpty();

            RuleFor(x => x.LastName)
                .Length(3, 20)
                .NotEmpty();

            RuleFor(x => x.PhoneNumber)
                .Length(9,9)
                .Must(phoneNumber => IsDigit(phoneNumber))
                .NotEmpty();

            RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage("Email ma niepoprawny format")
                .NotEmpty();            

            RuleFor(x => x.AddressLine1)
                .Length(1, 25)
                .NotEmpty();

            RuleFor(x => x.AddressLine2)
                .Length(1, 25)
                .NotEmpty();

            RuleFor(x => x.ZipCode)
                .Length(6, 6)
                .NotEmpty();
        }

        private bool IsDigit(string input)
        {
            foreach (char c in input)
            {
                if(c < '0' || c > '9')
                {
                    return false;
                }
            }

            return true;
        }
    }
}
