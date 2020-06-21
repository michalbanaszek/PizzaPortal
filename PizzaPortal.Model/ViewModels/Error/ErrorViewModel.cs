namespace PizzaPortal.Model.ViewModels.Error
{
    public class ErrorViewModel : BaseViewModel
    {
        public ErrorViewModel()
        {
            PageTitle = "Error Page";
        }

        public string ErrorTitle { get; set; }
        public string ErrorMessage { get; set; }
    }
}
