using PizzaPortal.Model.Models;
using System.Collections.Generic;

namespace PizzaPortal.DAL.Repositories.Abstract
{
    public interface IPizzaRepository : IRepository<Pizza>
    {
        IEnumerable<Pizza> PreferredPizzas { get; }
        IEnumerable<Pizza> GetAllByCategory(string category);
    }
}
