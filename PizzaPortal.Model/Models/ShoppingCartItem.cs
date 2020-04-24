

namespace PizzaPortal.Model.Models
{
    public class ShoppingCartItem : BaseModel
    {
        public Pizza Pizza { get; set; }
        public int Amount { get; set; }
        public string ShoppingCartId { get; set; }
    }
}
