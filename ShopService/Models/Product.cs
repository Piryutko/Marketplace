using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ShopService.Models
{
    public class Product
    {
        public Product(Guid shoppId, Guid itemId, string name, decimal cost, int quantity)
        {
            ProductId = Guid.NewGuid();
            ShoppId = shoppId;
            ItemId = itemId;
            Name = name;
            Cost = cost;
            Quantity = quantity;
            Price = cost;
        }

        [Key]
        public Guid ProductId { get; set; }

        public Guid ShoppId { get; set; }

        public Guid ItemId { get; set; }
        
        public string Name { get; set; }

        public decimal Cost {get; set; }

        public int Quantity { get; set; }

        public decimal Price {get; set; }

        public void ChangeQuantity(int quantity)
        {
            Quantity = quantity;
        }

        public void ChangeCost(int quantity)
        {
            Cost = default;
            Cost = Price * quantity;
        }

    }
}