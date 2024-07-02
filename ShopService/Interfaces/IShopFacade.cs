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

        IEnumerable<Item> GetItemsCategorySortByCostDescending(int categoryId);

        Response CreateShoppCart(Guid itemId, int quantity);

        bool UpdateShoppingCart(Guid shoppId, Guid itemId, int quantity);

        ShoppingCart GetShoppingCartById(Guid id);

        IEnumerable<Product> GetAllProductsByShoppId(Guid shoppId);

        bool TryUpdateProduct(Guid shoppId, Guid productId, int quantity);

        bool DeleteProductById(Guid shoppId, Guid productId);

        bool DeleteShoppCart(Guid shoppId);

        Order CreateOrder(string nickname, Guid shoppId);
    }
}