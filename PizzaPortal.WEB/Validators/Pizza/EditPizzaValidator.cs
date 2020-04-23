using FluentValidation;
using PizzaPortal.Model.ViewModels.Pizza;

namespace PizzaPortal.WEB.Validators.Pizza
{
    public class EditPizzaValidator : AbstractValidator<EditPizzaViewModel>
    {
        public EditPizzaValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Price).NotEmpty();
        }
    }
}
