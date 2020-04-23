using PizzaPortal.Model.Models;
using System.Threading.Tasks;

namespace PizzaPortal.DAL.Repositories.Abstract
{
    public interface ICategoryRepository : IRepository<CategoryDTO>
    {
        Task<CategoryDTO> GetByCategoryAsync(string category);
    }
}
