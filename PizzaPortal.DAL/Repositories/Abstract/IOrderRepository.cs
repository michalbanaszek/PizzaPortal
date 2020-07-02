using PizzaPortal.Model.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PizzaPortal.DAL.Repositories.Abstract
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<Order> GetOrderByIdWithInclude(string orderId);
        Task<List<Order>> GetUserOrdersAsync(string userId);
        Task<List<Order>> GetOrdersAsync();
        Task CreateOrderWithDetailsAsync(Order order);
    }
}
