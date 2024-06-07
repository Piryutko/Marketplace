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

        public bool CheckQuantityItem(Guid id, int quantity, out decimal cost, out string itemName);
        
        public bool TryAddItemInShoppCart(Guid id, int quantity, out decimal cost, out string itemName);

        public bool BuyItem(Guid itemId, int quantity);
    }
}