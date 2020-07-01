using PizzaPortal.BLL.Services.Concrete;
using PizzaPortal.Model.Models;
using System.Threading.Tasks;

namespace PizzaPortal.BLL.Services.Abstract
{
    public interface IIngredientService : IService<Ingredient>
    {
        Task<bool> CheckByNameAsync(string name);
    }
}
