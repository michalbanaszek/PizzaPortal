namespace PizzaPortal.Model.Models
{
    public class OrderDetail : BaseModel
    {
        public string PizzaId { get; set; }
        public string OrderId { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public virtual Pizza Pizza { get; set; }
        public virtual Order Order { get; set; }
    }
}
