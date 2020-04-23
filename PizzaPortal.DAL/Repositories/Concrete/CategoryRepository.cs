using Microsoft.EntityFrameworkCore;
using PizzaPortal.DAL.Repositories.Abstract;
using PizzaPortal.Migrations;
using PizzaPortal.Model.Models;
using System.Threading.Tasks;

namespace PizzaPortal.DAL.Repositories.Concrete
{
    public class CategoryRepository : Repository<CategoryDTO>, ICategoryRepository
    {
        private readonly DataContext _context;

        public CategoryRepository(DataContext context) : base(context)
        {
            this._context = context;
        }

        public async Task<CategoryDTO> GetByCategoryAsync(string category)
        {
            return await this._context.Categories.SingleOrDefaultAsync(x => x.Name.Equals(category));
        }
    }
}
