using FluentValidation;
using PizzaPortal.Model.ViewModels.Ingredient;

namespace PizzaPortal.WEB.Validators.Pizza
{
    public class IngredientEditValidator : AbstractValidator<IngredientEditViewModel>
    {
        public IngredientEditValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
