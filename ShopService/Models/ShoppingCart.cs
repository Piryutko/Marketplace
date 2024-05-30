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
            Items = default;
            Quantity = default;
            Cost = decimal.Zero;
        }

        [Key]
        public Guid Id { get; set; }

        public string Items { get; set;}

        public decimal Cost { get; set; }

        public int Quantity { get; set; }


        public void RefreshShoppingCart(decimal addedCost, string nameItem, int addedQuantity)
        {
            AddCost(addedCost);
            AddItems(nameItem);
            AddQuantity(addedQuantity);
        }

        private void AddCost(decimal addedCost)
        {
            Cost += addedCost;
        }

        private void AddItems(string itemName)
        {
            if(Items == default)
            {
                Items += itemName;
                return;
            }
            Items += ", " + itemName;
        }

        private void AddQuantity(int addedQuantity)
        {
            Quantity += addedQuantity;
        }

        public string GetItemsList()
        {
            return Items;
        }
    }
}