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

        public Guid OrderId { get; private set;}
        
        public string Nickname { get; private set;}
        
        public Guid ShoppId { get; private set;}

        public int Products { get; private set;}

        public decimal Cost { get; private set;}

        public string OrderInfo { get; private set; }

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