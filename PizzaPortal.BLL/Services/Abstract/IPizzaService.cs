using PizzaPortal.Model.Models;
using System.Collections.Generic;

namespace PizzaPortal.BLL.Services.Abstract
{
    public interface IPizzaService : IService<Pizza>
    {
        IEnumerable<Pizza> PreferredPizzas { get; }
        IEnumerable<Pizza> GetAllByCategory(string category);
    }
}
