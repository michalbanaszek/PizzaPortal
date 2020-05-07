using FluentValidation;
using PizzaPortal.Model.ViewModels.Category;

namespace PizzaPortal.WEB.Validators.Category
{
    public class CategoryCreateValidator : AbstractValidator<CategoryCreateViewModel>
    {
        public CategoryCreateValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
