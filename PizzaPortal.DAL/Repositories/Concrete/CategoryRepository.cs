using Microsoft.EntityFrameworkCore;
using PizzaPortal.DAL.Repositories.Abstract;
using PizzaPortal.Migrations;
using PizzaPortal.Model.Models;
using System.Threading.Tasks;

namespace PizzaPortal.DAL.Repositories.Concrete
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly DataContext _context;

        public CategoryRepository(DataContext context) : base(context)
        {
            this._context = context;
        }

        public async Task<Category> GetByCategoryAsync(string category)
        {
            return await this._context.Categories.SingleOrDefaultAsync(x => x.Name.Equals(category));
        }
    }
}
