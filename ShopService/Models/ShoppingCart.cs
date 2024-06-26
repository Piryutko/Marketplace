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
        public Guid Id { get; private set; }

        public decimal Cost { get; private set; }

        public int Quantity { get; private set; }



        public void UpdateShoppingCart(decimal addedCost, int addedQuantity)
        {
            AddCost(addedCost);
            AddQuantity(addedQuantity);
        }

        public void RefreshShoppingCart(int quantity, decimal cost)
        {
            RefreshCost(cost);
            RefreshQuantity(quantity);
        }

        private void AddCost(decimal addedCost)
        {
            Cost += addedCost;
        }


        private void AddQuantity(int addedQuantity)
        {
            Quantity += addedQuantity;
        }

        private void RefreshCost(decimal cost)
        {
            Cost = cost;
        }

        private void RefreshQuantity(int quantity)
        {
            Quantity = quantity;
        }
    }
}