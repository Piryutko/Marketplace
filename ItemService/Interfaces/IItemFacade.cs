using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ItemService.Enums;
using ItemService.Models;

namespace ItemService.Interfaces
{
    public interface IItemFacade
    {
        public bool TryAddItem(Item item);

        IList<Item> GetItemsByCategory(Category category);

        public IEnumerable<Item> GetItemsCategorySortByCost(Category category);

        IEnumerable<Item> GetItemsCategorySortByCostDescending(Category category);

        bool TryDeleteItem(Guid id);

        bool CheckQuantityItem(Guid id, int value);
    }
}