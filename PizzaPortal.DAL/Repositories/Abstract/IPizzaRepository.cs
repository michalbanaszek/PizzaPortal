using PizzaPortal.Model.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PizzaPortal.DAL.Repositories.Abstract
{
    public interface IPizzaRepository : IRepository<Pizza>
    {
        IEnumerable<Pizza> PreferredPizzas { get; }
        Task<IEnumerable<Pizza>> GetAllByCategoryAsync(string category);
        Task<IEnumerable<Pizza>> GetAllIncludedAsync();
    }
}
