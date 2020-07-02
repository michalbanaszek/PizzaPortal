using Microsoft.EntityFrameworkCore;
using PizzaPortal.DAL.Repositories.Abstract;
using PizzaPortal.Migrations;
using PizzaPortal.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaPortal.DAL.Repositories.Concrete
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private readonly DataContext _context;
        private readonly IShoppingCartRepository _shoppingCartRepository;

        public OrderRepository(DataContext context, IShoppingCartRepository shoppingCartRepository) : base(context)
        {
            this._context = context;
            this._shoppingCartRepository = shoppingCartRepository;
        }

        public async Task<List<Order>> GetOrdersAsync()
        {
            return await this._context.Orders.Include(x => x.OrderDetails)
                                             .Include(x => x.User)
                                             .OrderBy(x => x.OrderPlaced)
                                             .ToListAsync();
        }

        public async Task<Order> GetOrderByIdWithInclude(string orderId)
        {
            return await this._context.Orders.Include(x => x.OrderDetails)
                                                .ThenInclude(x => x.Pizza)
                                             .Include(x => x.User)
                                             .FirstOrDefaultAsync(x => x.Id == orderId);
        }

        public async Task<List<Order>> GetUserOrdersAsync(string userId)
        {
            return await this._context.Orders.Include(x => x.OrderDetails)
                                             .Include(x => x.User)
                                             .Where(x => x.UserId == userId)
                                             .OrderBy(x => x.OrderPlaced)
                                             .ToListAsync();
        }

        public async Task CreateOrderWithDetailsAsync(Order order)
        {
            order.OrderPlaced = DateTime.Now;
            decimal totalPrice = 0M;

            var cartItems = await this._shoppingCartRepository.GetShoppingCartItemsAsync();

            foreach (var item in cartItems)
            {
                var orderDetail = new OrderDetail()
                {
                    Order = order,
                    PizzaId = item.Pizza.Id,
                    Amount = item.Amount,
                    Price = item.Pizza.Price
                };

                totalPrice += orderDetail.Price * orderDetail.Amount;
                this._context.OrderDetails.Add(orderDetail);
            }

            order.OrderTotal = totalPrice;
            await base.CreateAsync(order);

            await this._context.SaveChangesAsync();
        }
    }
}
