using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ShopService.Models
{
    public class Order
    {
        public Order(string nickname, Guid shoppId, int sumProducts, decimal amount)
        {
            Nickname = nickname;
            ShoppId = shoppId;
            SumProducts = sumProducts;
            Amount = amount;
            OrderId = Guid.NewGuid();
            // PaymentStatus = false;
        }

        public Guid OrderId { get; set; }
        
        public string Nickname { get; set; }
        
        public Guid ShoppId { get; set; }

        public int SumProducts { get; set; }

        public decimal Amount { get; set; }

        // public bool PaymentStatus { get; set; }

        // public void ChangePaymentStatus(bool value)
        // {
        //     PaymentStatus = value;
        // }
    }
}