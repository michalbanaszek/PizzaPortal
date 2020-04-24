using Microsoft.EntityFrameworkCore;
using PizzaPortal.DAL.Repositories.Abstract;
using PizzaPortal.Migrations;
using PizzaPortal.Model.Models;
using System.Collections.Generic;
using System.Linq;

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

        public IEnumerable<Pizza> GetAllByCategory(string category)
        {
            return this._context.Pizzas.Where(x => x.Category.Name.Equals(category));
        }
    }
}
