using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ItemService.Enums;
using ItemService.Interfaces;
using ItemService.Models;

namespace ItemService.Facades
{
    public class ItemFacade : IItemFacade
    {
        private readonly IItemRepository _itemRepository;

        public ItemFacade(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public bool CheckQuantityItem(Guid id, int value)
        {
            return _itemRepository.CheckQuantityItem(id,value);
        }

        public IList<Item> GetItemsByCategory(Category category)
        {
            return _itemRepository.GetItemsByCategory(category);
        }

        public IEnumerable<Item> GetItemsCategorySortByCost(Category category)
        {
            return _itemRepository.GetItemsCategorySortByCost(category);
        }

        public IEnumerable<Item> GetItemsCategorySortByCostDescending(Category category)
        {
            return _itemRepository.GetItemsCategorySortByCostDescending(category);
        }

        public bool TryAddItem(Item item)
        {
            return _itemRepository.TryAddItem(item);
        }

        public bool TryDeleteItem(Guid id)
        {
            return _itemRepository.TryDeleteItem(id);
        }
    }
}