using System.Collections.Generic;

namespace PizzaPortal.Model.Models
{
    public class Category : BaseModel
    {       
        public string Name { get; set; }

        public virtual ICollection<Pizza> Pizzas { get; set; }
    }
}
