using PizzaPortal.Model.Models;
using System.Threading.Tasks;

namespace PizzaPortal.DAL.Repositories.Abstract
{
    public interface IIngredientRepository : IRepository<Ingredient>
    {
        Task<bool> CheckByNameAsync(string name);
    }
}
