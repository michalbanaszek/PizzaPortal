using PizzaPortal.Model.Models;
using System.Threading.Tasks;

namespace PizzaPortal.BLL.Services.Abstract
{
    public interface IOrderService : IService<Order>
    {
        Task NewOrderAsync(Order order);
    }
}
