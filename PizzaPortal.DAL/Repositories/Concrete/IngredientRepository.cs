using Microsoft.EntityFrameworkCore;
using PizzaPortal.DAL.Repositories.Abstract;
using PizzaPortal.Migrations;
using PizzaPortal.Model.Models;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaPortal.DAL.Repositories.Concrete
{
    public class IngredientRepository : Repository<Ingredient>, IIngredientRepository
    {
        private readonly DataContext _context;

        public IngredientRepository(DataContext context) : base(context)
        {
            this._context = context;
        }

        public async Task<bool> CheckByNameAsync(string name)
        {
            return await this._context.Ingredients.Where(x => x.Name.ToLower() == name.ToLower()).AnyAsync();
        }
    }
}
