using Microsoft.EntityFrameworkCore;
using PizzaPortal.DAL.Repositories.Abstract;
using PizzaPortal.Migrations;
using PizzaPortal.Model.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaPortal.DAL.Repositories.Concrete
{
    public class PizzaRepository : Repository<Pizza>, IPizzaRepository
    {
        private readonly DataContext _context;

        public PizzaRepository(DataContext context) : base(context)
        {
            this._context = context;
        }

        public IEnumerable<Pizza> PreferredPizzas => this._context.Pizzas.Where(x => x.IsPreferredPizza).Include(x => x.Category);

        public async Task<IEnumerable<Pizza>> GetAllByCategoryAsync(string category)
        {
            return await this._context.Set<Pizza>().Where(x => x.Category.Name.Equals(category)).Include(x => x.Category).ToListAsync();;
        }

        public async Task<IEnumerable<Pizza>> GetAllIncludedAsync()
        {
            return await this._context.Set<Pizza>().Include(x => x.Category).ToListAsync();
           
        }
    }
}
