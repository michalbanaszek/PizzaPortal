using PizzaPortal.Model.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PizzaPortal.BLL.Services.Abstract
{
    public interface IOrderService : IService<Order>
    {
        Task<Order> GetOrderSummaryByIdAsync(string orderId);
        Task<List<Order>> GetUserOrdersAsync(string userId);
        Task<List<Order>> GetOrdersAsync();
        Task NewOrderAsync(Order order);
    }
}
