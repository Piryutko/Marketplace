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

        public void AddToCart(int id)
        {
            throw new NotImplementedException();
        }

        public Order AddUserInPayment()
        {
            throw new NotImplementedException();
        }

        public Order CreateOrder()
        {
            throw new NotImplementedException();
        }

        public string GetReceiptPayment(Order order)
        {
            throw new NotImplementedException();
        }

        public bool TryDeleteAllItemInCart()
        {
            throw new NotImplementedException();
        }

        public bool TryDeleteItemInCart(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}