using PizzaPortal.BLL.Services.Abstract;
using PizzaPortal.DAL.Repositories.Abstract;
using PizzaPortal.Model.Models;
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

        public async Task NewOrderAsync(Order order)
        {
            await this._orderRepository.NewOrderAsync(order);
        }
    }
}
