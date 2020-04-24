using PizzaPortal.Model.Models;
using System.Threading.Tasks;

namespace PizzaPortal.DAL.Repositories.Abstract
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task NewOrderAsync(Order order);
    }
}
