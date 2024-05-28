using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopService.Models
{
    public class Order
    {
        public Order(string userNickname)
        {
            UserNickname = userNickname;
            Items = new List<ShoppingCart>();
        }

        public string UserNickname { get; set; }
        
        public List<ShoppingCart> Items { get; set; }
        
        public decimal AmountPurchases { get; set; }

        public void AddItems(ShoppingCart items)
        {
            Items.Add(items);
            AddAmountPurchases(items.Cost);
        }

        private void AddAmountPurchases(decimal cost)
        {
            AmountPurchases += cost;
        }


    }
}