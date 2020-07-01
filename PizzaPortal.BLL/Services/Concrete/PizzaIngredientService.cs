using PizzaPortal.BLL.Services.Abstract;
using PizzaPortal.DAL.Repositories.Abstract;
using PizzaPortal.Model.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PizzaPortal.BLL.Services.Concrete
{
    public class PizzaIngredientService : Service<PizzaIngredient>, IPizzaIngredientService
    {
        private readonly IPizzaIngredientRepository _pizzaIngredientRepository;

        public PizzaIngredientService(IPizzaIngredientRepository pizzaIngredientRepository) : base(pizzaIngredientRepository)
        {
            this._pizzaIngredientRepository = pizzaIngredientRepository;
        }

        public async Task<bool> CheckIngredientIsExistInPizza(string pizzaId, string ingredientId)
        {
            return await this._pizzaIngredientRepository.CheckIngredientIsExistInPizza(pizzaId, ingredientId);
        }

        public async Task<List<PizzaIngredient>> GetByAllWithInclude()
        {
           return await this._pizzaIngredientRepository.GetByAllWithInclude();
        }

        public async Task<PizzaIngredient> GetByIdWithInclude(string id)
        {
            return await this._pizzaIngredientRepository.GetByIdWithInclude(id);
        }
    }
}
