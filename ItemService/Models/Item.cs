using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnsureThat;
using ItemService.Enums;

namespace ItemService.Models
{
    public class Item
    {
        public Item(string name, Category category, decimal cost, int quantity)
        {
            try
            {
                Ensure.That(name).IsNotNullOrWhiteSpace();
                Ensure.String.Matches(name, @"^[a-zA-Z]+$");
                Name = name;

                Id = Guid.NewGuid();

                Category = category;

                Ensure.That(cost).IsGt(0);
                Cost = cost;

                Quantity = quantity;
            }
            catch (Exception ex)
            {
                Id = Guid.Empty;
                Name = String.Empty;
                Cost = 0;
                Quantity = 0;
                Console.WriteLine($"Exception: {ex.Message}");
            }
        }

        public string Name { get; set; }

        public Guid Id { get; set; }

        public Category Category { get; set; }

        public decimal Cost { get; set;}

        public int Quantity { get; set; }

        public bool TryReduceQuantity(int value)
        {
            if(Quantity != 0)
            {
                Quantity = Quantity - value;
                return true;
            }
            return false;
        }

        public int GetQuantity()
        {
            return Quantity;
        }

        public void ChangeQuantity(int value)
        {
            Quantity = Quantity - value;
        }
    }
}