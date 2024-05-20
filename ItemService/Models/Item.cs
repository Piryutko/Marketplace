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

                Id = Guid.NewGuid();

                Category = category;

                Ensure.That(cost).IsGt(0);
                Cost = cost;

                Ensure.That(quantity).IsGt(0);
                Quantity = quantity;
            }
            catch (Exception ex)
            {
                Id = Guid.Empty;
                Name = String.Empty;
                Cost = 0;
                Quantity = -1;
                Console.WriteLine($"Exception: {ex.Message}");
            }
        }

        public string Name { get; }

        public Guid Id { get; }

        public Category Category { get; }

        public decimal Cost { get; }

        public int Quantity { get; private set; }

        public bool TryReduceQuantity(int value)
        {
            if(Quantity != 0)
            {
                Quantity = Quantity - value;
                return true;
            }
            return false;
        }
    }
}