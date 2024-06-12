using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ShopService.Models
{
    public class Order
    {
        public Order(string nickname, Guid shoppId, int products, decimal cost)
        {
            Nickname = nickname;
            ShoppId = shoppId;
            Products = products;
            Cost = cost;
            OrderId = Guid.NewGuid();
            
            // PaymentStatus = false;
        }

        public Guid OrderId { get; set; }
        
        public string Nickname { get; set; }
        
        public Guid ShoppId { get; set; }

        public int Products { get; set; }

        public decimal Cost { get; set; }

        public string OrderInfo { get; set; }

        public string AddInfo(string message)
        {
            OrderInfo += message;
            return OrderInfo;
        }

        // public bool PaymentStatus { get; set; }

        // public void ChangePaymentStatus(bool value)
        // {
        //     PaymentStatus = value;
        // }
    }
}