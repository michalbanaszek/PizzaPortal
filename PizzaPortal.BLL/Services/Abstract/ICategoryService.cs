using PizzaPortal.Model.Models;
using System.Threading.Tasks;

namespace PizzaPortal.BLL.Services.Abstract
{
    public interface ICategoryService : IService<Category>
    {
        Task<Category> GetByCategoryAsync(string category);
    }
}
