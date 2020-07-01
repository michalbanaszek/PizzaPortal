namespace PizzaPortal.Model.Models
{
    public class PizzaIngredient : BaseModel
    {     
        public string PizzaId { get; set; }
     
        public string IngredientId { get; set; }

        public virtual Ingredient Ingredient { get; set; }
        public virtual Pizza Pizza { get; set; }
    }
}
