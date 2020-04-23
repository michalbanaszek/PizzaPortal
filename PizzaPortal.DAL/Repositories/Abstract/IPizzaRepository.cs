using PizzaPortal.Model.Models;
using System.Collections.Generic;

namespace PizzaPortal.DAL.Repositories.Abstract
{
    public interface IPizzaRepository : IRepository<PizzaDTO>
    {
        IEnumerable<PizzaDTO> PreferredPizzas { get; }
        IEnumerable<PizzaDTO> GetAllByCategory(string category);
    }
}
