using FluentValidation;
using PizzaPortal.Model.ViewModels.Category;

namespace PizzaPortal.WEB.Validators.Category
{
    public class CategoryEditValidator : AbstractValidator<CategoryEditViewModel>
    {
        public CategoryEditValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
