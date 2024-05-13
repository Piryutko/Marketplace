using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopService.Models;

namespace ShopService.Interfaces
{
    public interface IOrderRepository
    {
        public bool TryAddCart(int id);

        public void AddUserInPayment();

        public bool TryDeleteItemInCart(int numberItem);

        public bool TryDeleteAllItemInCart();

        public Order GetReceiptPayment();
    }
}