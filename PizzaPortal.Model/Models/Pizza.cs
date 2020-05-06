namespace PizzaPortal.Model.Models
{
    public class Pizza : BaseModel
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public bool IsPreferredPizza { get; set; }
        public string PhotoPath { get; set; }
        public string CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
