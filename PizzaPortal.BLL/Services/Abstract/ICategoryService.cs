using PizzaPortal.Model.Models;
using System.Threading.Tasks;

namespace PizzaPortal.BLL.Services.Abstract
{
    public interface ICategoryService : IService<CategoryDTO>
    {
        Task<CategoryDTO> GetByCategoryAsync(string category);
    }
}
