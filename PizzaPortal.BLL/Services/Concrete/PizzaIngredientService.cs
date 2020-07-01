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

        public async Task<bool> CheckIngredientIsExistInPizzaAsync(string pizzaId, string ingredientId)
        {
            return await this._pizzaIngredientRepository.CheckIngredientIsExistInPizzaAsync(pizzaId, ingredientId);
        }

        public async Task<List<string>> GetAllIngredientInPizzaAsync(string pizzaId)
        {
            return await this._pizzaIngredientRepository.GetAllIngredientInPizzaAsync(pizzaId);
        }

        public async Task<List<PizzaIngredient>> GetByAllWithIncludeAsync()
        {
           return await this._pizzaIngredientRepository.GetByAllWithIncludeAsync();
        }

        public async Task<PizzaIngredient> GetByIdWithIncludeAsync(string id)
        {
            return await this._pizzaIngredientRepository.GetByIdWithIncludeAsync(id);
        }

        public async Task<bool> RemoveAllIngredientInPizzaAsync(string pizzaId)
        {
            return await this._pizzaIngredientRepository.RemoveAllIngredientInPizzaAsync(pizzaId);
        }
    }
}
