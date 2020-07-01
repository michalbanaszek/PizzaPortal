using Microsoft.EntityFrameworkCore;
using PizzaPortal.DAL.Repositories.Abstract;
using PizzaPortal.Migrations;
using PizzaPortal.Model.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaPortal.DAL.Repositories.Concrete
{
    public class PizzaIngredientRepository : Repository<PizzaIngredient>, IPizzaIngredientRepository
    {
        private readonly DataContext _context;

        public PizzaIngredientRepository(DataContext context) : base(context)
        {
            this._context = context;
        }

        public async Task<bool> CheckIngredientIsExistInPizzaAsync(string pizzaId, string ingredientId)
        {
            var pizzas = await this._context.PizzaIngredients.Where(x => x.PizzaId == pizzaId).ToListAsync();

            foreach (var item in pizzas)
            {
                if (item.IngredientId == ingredientId)
                {
                    return true;
                }
            }

            return false;
        }

        public async Task<List<string>> GetAllIngredientInPizzaAsync(string pizzaId)
        {
            var pizzas = await this._context.PizzaIngredients.Where(x => x.PizzaId == pizzaId).Include(x => x.Ingredient).ToListAsync();

            var ingredients = pizzas.Select(x => x.Ingredient.Name).ToList();

            return ingredients;
        }

        public async Task<List<PizzaIngredient>> GetByAllWithIncludeAsync()
        {
            return await this._context.PizzaIngredients.OrderBy(x => x.PizzaId).Include(x => x.Ingredient).Include(x => x.Pizza).ToListAsync();
        }

        public async Task<PizzaIngredient> GetByIdWithIncludeAsync(string id)
        {
            return await this._context.PizzaIngredients.Include(x => x.Pizza).Include(x => x.Ingredient).SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> RemoveAllIngredientInPizzaAsync(string pizzaId)
        {
            var pizzaIngredients = await this._context.PizzaIngredients.Where(x => x.PizzaId == pizzaId).ToListAsync();

            this._context.PizzaIngredients.RemoveRange(pizzaIngredients);

            int result = await this._context.SaveChangesAsync();

            return result > 0 ? true : false;
        }
    }
}
