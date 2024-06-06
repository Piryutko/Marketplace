using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopService.Models;

namespace ShopService.Interfaces
{
    public interface IShoppingCartRepository
    {
        public Guid CreateShoppingCart();

        public ShoppingCart GetShoppingCartById(Guid id);

        public bool UpdateShoppingCart(Guid shoppCartId, decimal addedCost, int addedQuantity);

        public void RefreshShoppingCart(Guid shoppCartId, int quantity, decimal cost);
        

        public void SaveChanges();
    }
}