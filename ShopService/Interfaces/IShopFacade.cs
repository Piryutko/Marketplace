using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopService.Models;

namespace ShopService.Interfaces
{
    public interface IShopFacade
    {
        bool CheckNickname(string name);

        IEnumerable<Item> GetItemsByCategory(int categoryId);

        IEnumerable<Item> GetItemsCategorySortByCost(int categoryId);
    }
}