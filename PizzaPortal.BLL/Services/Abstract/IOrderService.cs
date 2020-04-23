using PizzaPortal.Model.Models;
using System.Threading.Tasks;

namespace PizzaPortal.BLL.Services.Abstract
{
    public interface IOrderService : IService<OrderDTO>
    {
        Task NewOrderAsync(OrderDTO order);
    }
}
