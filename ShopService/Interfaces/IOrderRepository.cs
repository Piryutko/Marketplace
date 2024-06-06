using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopService.Models;

namespace ShopService.Interfaces
{
    public interface IOrderRepository
    {
        public Order CreateOrder(string nickname, Guid shoppId, int sumProducts, decimal amount);

        public Order GetOrderById(Guid id);
    }
}