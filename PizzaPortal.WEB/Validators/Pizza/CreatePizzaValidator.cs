using FluentValidation;
using PizzaPortal.Model.ViewModels.Pizza;

namespace PizzaPortal.WEB.Validators.Pizza
{
    public class CreatePizzaValidator : AbstractValidator<CreatePizzaViewModel>
    {
        public CreatePizzaValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Price).NotEmpty();
        }
    }
}
