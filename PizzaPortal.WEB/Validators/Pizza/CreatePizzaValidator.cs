using FluentValidation;
using PizzaPortal.Model.ViewModels.Pizza;

namespace PizzaPortal.WEB.Validators.Pizza
{
    public class CreatePizzaValidator : AbstractValidator<PizzaCreateViewModel>
    {
        public CreatePizzaValidator()
        {
            RuleFor(x => x.Name)
                .Length(3, 20)
                .NotEmpty();

            RuleFor(x => x.Price)
                .NotEmpty();            

            RuleFor(x => x.CategoryId)
                .NotEmpty()
                .NotNull();
        }
    }
}
