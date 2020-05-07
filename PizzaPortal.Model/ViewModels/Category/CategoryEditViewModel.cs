namespace PizzaPortal.Model.ViewModels.Category
{
    public class CategoryEditViewModel : CategoryCreateViewModel
    {
        public CategoryEditViewModel()
        {           
                PageTitle = "Edit Category";
        }

        public string Id { get; set; }
    
    }
}
