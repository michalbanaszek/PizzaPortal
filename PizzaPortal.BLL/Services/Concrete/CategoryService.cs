using PizzaPortal.BLL.Services.Abstract;
using PizzaPortal.DAL.Repositories.Abstract;
using PizzaPortal.Model.Models;
using System.Threading.Tasks;

namespace PizzaPortal.BLL.Services.Concrete
{
    public class CategoryService : Service<Category>, ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository) : base(categoryRepository)
        {
            this._categoryRepository = categoryRepository;
        }

        public async Task<Category> GetByCategoryAsync(string category)
        {
            return await this.GetByCategoryAsync(category);
        }
    }
}
