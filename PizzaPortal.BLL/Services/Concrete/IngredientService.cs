using PizzaPortal.BLL.Services.Abstract;
using PizzaPortal.DAL.Repositories.Abstract;
using PizzaPortal.Model.Models;
using System.Threading.Tasks;

namespace PizzaPortal.BLL.Services.Concrete
{
    public class IngredientService : Service<Ingredient>, IIngredientService
    {
        private readonly IIngredientRepository _ingredientRepository;

        public IngredientService(IIngredientRepository ingredientRepository) : base(ingredientRepository)
        {
            this._ingredientRepository = ingredientRepository;
        }

        public async Task<bool> CheckByNameAsync(string name)
        {
            return await this._ingredientRepository.CheckByNameAsync(name);
        }
    }
}
