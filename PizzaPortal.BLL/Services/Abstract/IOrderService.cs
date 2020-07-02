using PizzaPortal.Model.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PizzaPortal.BLL.Services.Abstract
{
    public interface IOrderService : IService<Order>
    {
        Task<Order> GetOrderByIdWithInclude(string orderId);
        Task<List<Order>> GetUserOrdersAsync(string userId);
        Task<List<Order>> GetOrdersAsync();
        Task CreateOrderWithDetailsAsync(Order order);
    }
}
