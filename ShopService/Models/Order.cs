using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopService.Models
{
    public class Order
    {
        public string UserNickname { get; set; }
        
        public List<Item> Items { get; set; }
        
        public decimal AmountPurchases {get; set; }
    }
}