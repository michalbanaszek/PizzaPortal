using PizzaPortal.Model.Models;
using System.Threading.Tasks;

namespace PizzaPortal.DAL.Repositories.Abstract
{
    public interface IOrderRepository : IRepository<OrderDTO>
    {
        Task NewOrderAsync(OrderDTO order);
    }
}
