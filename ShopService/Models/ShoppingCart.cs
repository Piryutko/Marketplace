using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ShopService.Models
{
    public class ShoppingCart
    {
        public ShoppingCart()
        {
            Id = Guid.NewGuid();
            Quantity = default;
            Cost = decimal.Zero;
        }

        [Key]
        public Guid Id { get; set; }

        public decimal Cost { get; set; }

        public int Quantity { get; set; }



        public void UpdateShoppingCart(decimal addedCost, int addedQuantity)
        {
            AddCost(addedCost);
            AddQuantity(addedQuantity);
        }

        private void AddCost(decimal addedCost)
        {
            Cost += addedCost;
        }


        private void AddQuantity(int addedQuantity)
        {
            Quantity += addedQuantity;
        }
    }
}