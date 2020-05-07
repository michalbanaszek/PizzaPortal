namespace PizzaPortal.Model.ViewModels.Category
{
    public class CategoryDeleteViewModel : BaseViewModel
    {
        public CategoryDeleteViewModel()
        {
            PageTitle = "Delete Category";
        }

        public string Id { get; set; }
        public string Name { get; set; }
    }
}
