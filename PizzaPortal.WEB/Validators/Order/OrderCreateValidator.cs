using FluentValidation;
using PizzaPortal.Model.ViewModels.Order;

namespace PizzaPortal.WEB.Validators.Order
{
    public class OrderCreateValidator : AbstractValidator<OrderCreateViewModel>
    {
        public OrderCreateValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.PhoneNumber).NotEmpty();
            RuleFor(x => x.Email).EmailAddress().NotEmpty();
            RuleFor(x => x.AddressLine1).NotEmpty();
            RuleFor(x => x.AddressLine2).NotEmpty();
        }
    }
}
