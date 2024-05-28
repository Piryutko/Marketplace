using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopService.Models;

namespace ShopService.Interfaces
{
    public interface IShopClient
    {
        public bool GetResultRequestByNickname(string nickname, out string result);

        public IEnumerable<Item> GetItemsByCategory(int categoryId);

        public IEnumerable<Item> GetItemsCategorySortByCost(int categoryId);

        public IEnumerable<Item> GetItemsCategorySortByCostDescending(int categoryId);
    }
}