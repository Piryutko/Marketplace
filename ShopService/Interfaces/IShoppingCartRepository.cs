using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopService.Models;

namespace ShopService.Interfaces
{
    public interface IShoppingCartRepository
    {
         Guid CreateShoppingCart();

         ShoppingCart GetShoppingCartById(Guid id);

         bool UpdateShoppingCart(Guid shoppCartId, decimal addedCost, int addedQuantity);

         void RefreshShoppingCart(Guid shoppCartId, int quantity, decimal cost);
        
         void DeleteShoppingCart(Guid shoppCartId);

         void SaveChanges();
    }
}