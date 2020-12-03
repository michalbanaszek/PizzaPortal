using FluentValidation;
using PizzaPortal.Model.ViewModels.Ingredient;

namespace PizzaPortal.WEB.Validators.Ingredient
{
    public class IngredientEditValidator : AbstractValidator<IngredientEditViewModel>
    {
        public IngredientEditValidator()
        {
            RuleFor(x => x.Name)
                .Length(3, 20)
                .NotEmpty();
        }
    }
}
