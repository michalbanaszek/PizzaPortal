namespace PizzaPortal.Model.Models
{
    public class OrderDetailDTO : BaseModelDTO
    {
        public string PizzaId { get; set; }
        public string OrderId { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public virtual PizzaDTO Pizza { get; set; }
        public virtual OrderDTO Order { get; set; }
    }
}
