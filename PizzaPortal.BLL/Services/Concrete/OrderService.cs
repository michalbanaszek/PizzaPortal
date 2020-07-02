using PizzaPortal.BLL.Services.Abstract;
using PizzaPortal.DAL.Repositories.Abstract;
using PizzaPortal.Model.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PizzaPortal.BLL.Services.Concrete
{
    public class OrderService : Service<Order>, IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository) : base(orderRepository)
        {
            this._orderRepository = orderRepository;
        }

        public Task<List<Order>> GetOrdersAsync()
        {
            return this._orderRepository.GetOrdersAsync();
        }

        public Task<Order> GetOrderByIdWithInclude(string orderId)
        {
            return this._orderRepository.GetOrderByIdWithInclude(orderId);
        }

        public Task<List<Order>> GetUserOrdersAsync(string userId)
        {
            return this._orderRepository.GetUserOrdersAsync(userId);
        }

        public async Task CreateOrderWithDetailsAsync(Order order)
        {
            await this._orderRepository.CreateOrderWithDetailsAsync(order);
        }
    }
}
