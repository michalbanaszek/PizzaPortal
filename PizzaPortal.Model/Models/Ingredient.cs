using System.Collections.Generic;

namespace PizzaPortal.Model.Models
{
    public class Ingredient : BaseModel
    {
        public string Name { get; set; }

        public virtual ICollection<PizzaIngredient> PizzaIngredients { get; set; }
    }
}
