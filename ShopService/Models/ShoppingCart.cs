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
            Items = new List<Guid>();
            Quantity = default;
            Cost = decimal.Zero;
        }

        [Key]
        public Guid Id { get; set; }

        [NotMapped]
        public List<Guid> Items { get; set;}

        public decimal Cost { get; set; }

        public int Quantity { get; set; }


        public void RefreshShoppingCart(decimal addedCost, Guid idItem, int addedQuantity)
        {
            AddCost(addedCost);
            AddIdItems(idItem);
            AddQuantity(addedQuantity);
        }

        private void AddCost(decimal addedCost)
        {
            Cost += addedCost;
        }

        private void AddIdItems(Guid idItem)
        {
            Items.Add(idItem);
        }

        private void AddQuantity(int addedQuantity)
        {
            Quantity += addedQuantity;
        }

        public List<Guid> GetItemsList()
        {
            return Items;
        }
    }
}