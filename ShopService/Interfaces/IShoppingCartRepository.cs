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

        public bool UpdateShoppingCart(Guid IdShoppCart, decimal addedCost, Guid idItem, int addedQuantity);
    }
}