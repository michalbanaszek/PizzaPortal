using PizzaPortal.Model.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PizzaPortal.DAL.Repositories.Abstract
{
    public interface IPizzaIngredientRepository : IRepository<PizzaIngredient>
    {
        Task<List<PizzaIngredient>> GetByAllWithInclude();

        Task<PizzaIngredient> GetByIdWithInclude(string id);

        Task<bool> CheckIngredientIsExistInPizza(string pizzaId, string ingredientId);
    }
}
