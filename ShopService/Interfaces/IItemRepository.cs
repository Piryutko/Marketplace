using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopService.Enums;
using ShopService.Models;

namespace ShopService.Interfaces
{
    public interface IItemRepository
    {
        public IEnumerable<Item> GetItemsByCategory(int id);

        public IEnumerable<Item> SortItemPrice(int category);

        public IEnumerable<Item> SortItemPriceByDescending(int category);

    }
}