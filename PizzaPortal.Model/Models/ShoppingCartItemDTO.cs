

namespace PizzaPortal.Model.Models
{
    public class ShoppingCartItemDTO : BaseModelDTO
    {
        public PizzaDTO Pizza { get; set; }
        public int Amount { get; set; }
        public string ShoppingCartId { get; set; }
    }
}
