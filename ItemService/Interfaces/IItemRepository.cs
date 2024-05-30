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
        public bool TryAddItem(Item item);

        public IList<Item> GetItemsByCategory(Category category);

        public IEnumerable<Item> GetItemsCategorySortByCost(Category category);

        public IEnumerable<Item> GetItemsCategorySortByCostDescending(Category category);

        public bool TryDeleteItem(Guid id);

        public bool BuyItem(Guid id, int quantity);

        public decimal GetCostItem(Guid id);

        public string GetItemByName(Guid id);
    }
}