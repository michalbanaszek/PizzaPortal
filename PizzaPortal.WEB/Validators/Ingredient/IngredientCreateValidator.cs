using FluentValidation;
using PizzaPortal.Model.ViewModels.Ingredient;

namespace PizzaPortal.WEB.Validators.Ingredient
{
    public class IngredientCreateValidator : AbstractValidator<IngredientCreateViewModel>
    {
        public IngredientCreateValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
