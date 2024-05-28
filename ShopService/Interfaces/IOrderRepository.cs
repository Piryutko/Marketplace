using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopService.Models;

namespace ShopService.Interfaces
{
    public interface IOrderRepository
    {
        public void AddToCart(int id);

        public Order AddUserInPayment();

        public bool TryDeleteItemInCart(Guid id);

        public bool TryDeleteAllItemInCart();

        public string GetReceiptPayment(Order order); //получитть квитанцию в Json

        public Order CreateOrder();
    }
}