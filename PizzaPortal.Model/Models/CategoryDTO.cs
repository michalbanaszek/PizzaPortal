using System.Collections.Generic;

namespace PizzaPortal.Model.Models
{
    public class CategoryDTO : BaseModelDTO
    {       
        public string Name { get; set; }

        public virtual ICollection<PizzaDTO> Pizzas { get; set; }
    }
}
