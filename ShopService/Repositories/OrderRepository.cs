using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopService.Data;
using ShopService.Interfaces;
using ShopService.Models;

namespace ShopService.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public Order CreateOrder(Order order)
        {
           _context.Orders.Add(order);

           if(_context.SaveChanges() > 0)
           {
                return order;
           }
           
           return null;
        }

        public Order GetOrderById(Guid id)
        {
            return _context.Orders.FirstOrDefault(o => o.OrderId == id);
        }
    }
}