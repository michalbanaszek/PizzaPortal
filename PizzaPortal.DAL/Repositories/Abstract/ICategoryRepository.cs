using PizzaPortal.Model.Models;
using System.Threading.Tasks;

namespace PizzaPortal.DAL.Repositories.Abstract
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<Category> GetByCategoryAsync(string category);
    }
}
