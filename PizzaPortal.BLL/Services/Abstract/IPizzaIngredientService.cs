using PizzaPortal.Model.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PizzaPortal.BLL.Services.Abstract
{
    public interface IPizzaIngredientService : IService<PizzaIngredient>
    {
        Task<List<PizzaIngredient>> GetByAllWithInclude();
        Task<PizzaIngredient> GetByIdWithInclude(string id);
        Task<bool> CheckIngredientIsExistInPizza(string pizzaId, string ingredientId);
    }
}
