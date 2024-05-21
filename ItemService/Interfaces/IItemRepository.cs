using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ItemService.Enums;
using ItemService.Models;

namespace ItemService.Interfaces
{
    public interface IItemRepository
    {
        // public bool TryAddItem(string name, Category category, decimal cost, int quantity);

        public bool TryAddItem(Item item);

        public IEnumerable<Item> GetItemsByCategory(Category category);

        public IEnumerable<Item> GetItemsSortByCost(Category category);

        public IEnumerable<Item> GetItemsSortByCostDescending(Category category);

        public bool TryDeleteItem(Guid id);

        public bool BuyItem(Guid id, int quantity);
    }
}