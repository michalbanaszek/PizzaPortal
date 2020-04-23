namespace PizzaPortal.Model.Models
{
    public class PizzaDTO : BaseModelDTO
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public bool IsPreferredPizza { get; set; }
        public string CategoryId { get; set; }
        public virtual CategoryDTO Category { get; set; }
    }
}
