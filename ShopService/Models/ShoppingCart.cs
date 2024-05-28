using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopService.Models
{
    public class ShoppingCart
    {
        public ShoppingCart(Guid idItems, int quantity, decimal cost)
        {
            Id = Guid.NewGuid();
            IdItems = idItems;
            Quantity = quantity;
            Cost = cost;
        }

        public Guid Id { get; set; }

        public Guid IdItems { get; set; }

        public decimal Cost { get; set; }

        public int Quantity { get; set; }

    }
}