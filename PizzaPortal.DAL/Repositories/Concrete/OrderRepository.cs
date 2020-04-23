using PizzaPortal.DAL.Repositories.Abstract;
using PizzaPortal.Migrations;
using PizzaPortal.Model.Models;
using System;
using System.Threading.Tasks;

namespace PizzaPortal.DAL.Repositories.Concrete
{
    public class OrderRepository : Repository<OrderDTO>, IOrderRepository
    {
        private readonly DataContext _context;
        private readonly IShoppingCartRepository _shoppingCartRepository;

        public OrderRepository(DataContext context, IShoppingCartRepository shoppingCartRepository) : base(context)
        {
            this._context = context;
            this._shoppingCartRepository = shoppingCartRepository;
        }

        public async Task NewOrderAsync(OrderDTO order)
        {
            order.OrderPlaced = DateTime.Now;

            await base.CreateAsync(order);

            var cartItems = await this._shoppingCartRepository.GetShoppingCartItemsAsync();

            foreach (var item in cartItems)
            {
                var orderDetail = new OrderDetailDTO()
                {
                    OrderId = order.Id,
                    PizzaId = item.Pizza.Id,
                    Amount = item.Amount,
                    Price = item.Pizza.Price
                };

                this._context.OrderDetails.Add(orderDetail);
            }

            await this._context.SaveChangesAsync();
        }
    }
}
