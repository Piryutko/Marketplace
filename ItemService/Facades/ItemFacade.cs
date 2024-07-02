using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ItemService.Enums;
using ItemService.Exceptions;
using ItemService.Interfaces;
using ItemService.Models;
using Microsoft.Extensions.FileProviders;

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
            return _itemRepository.CheckQuantityItem(id, value);
        }

        public IList<Item> GetItemsByCategory(Category category)
        {
            var result = _itemRepository.GetItemsByCategory(category);
            return result.Any() ? result : throw new CategoryNotFoundException();
        }

        public IEnumerable<Item> GetItemsCategorySortByCost(Category category)
        {
            var result = _itemRepository.GetItemsByCategory(category);
            return result.Any() ? result : throw new CategoryNotFoundException();
        }

        public IEnumerable<Item> GetItemsCategorySortByCostDescending(Category category)
        {
            var result = _itemRepository.GetItemsCategorySortByCostDescending(category);
            return result.Any() ? result : throw new CategoryNotFoundException();
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