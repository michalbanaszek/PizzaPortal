namespace PizzaPortal.Model.ViewModels.Category
{
    public class CategoryCreateViewModel : BaseViewModel
    {
        public CategoryCreateViewModel()
        {
            PageTitle = "Create Category";
        }

        public string Name { get; set; }
    }
}
