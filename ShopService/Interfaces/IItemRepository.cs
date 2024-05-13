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
        public List<Item> GetItems(int id);

        public List<Item> SortItemPrice(Category category);

    }
}