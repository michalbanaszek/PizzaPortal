using FluentValidation;
using PizzaPortal.Model.ViewModels.Pizza;

namespace PizzaPortal.WEB.Validators.Pizza
{
    public class EditPizzaValidator : AbstractValidator<PizzaEditViewModel>
    {
        public EditPizzaValidator()
        {
            RuleFor(x => x.Name)
                .Length(3, 20)
                .NotEmpty();

            RuleFor(x => x.Price)
                .NotNull();

            RuleFor(x => x.CategoryId)
                .NotEmpty()
                .NotNull();
        }
    }
}
