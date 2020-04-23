using PizzaPortal.Model.Models;
using System.Collections.Generic;

namespace PizzaPortal.BLL.Services.Abstract
{
    public interface IPizzaService : IService<PizzaDTO>
    {
        IEnumerable<PizzaDTO> PreferredPizzas { get; }
        IEnumerable<PizzaDTO> GetAllByCategory(string category);
    }
}
