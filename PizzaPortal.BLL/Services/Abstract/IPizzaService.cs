using PizzaPortal.Model.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PizzaPortal.BLL.Services.Abstract
{
    public interface IPizzaService : IService<Pizza>
    {
        IEnumerable<Pizza> PreferredPizzas { get; }
        Task<IEnumerable<Pizza>> GetAllByCategoryAsync(string category);
        Task<IEnumerable<Pizza>> GetAllIncludedAsync();
    }
}
