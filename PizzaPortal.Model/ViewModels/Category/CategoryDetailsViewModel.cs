namespace PizzaPortal.Model.ViewModels.Category
{
    public class CategoryDetailsViewModel : BaseViewModel
    {
        public CategoryDetailsViewModel()
        {
            PageTitle = "Details Category";
        }

        public string Id { get; set; }
        public string Name { get; set; }
    }
}
