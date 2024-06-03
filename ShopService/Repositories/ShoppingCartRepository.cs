using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopService.Data;
using ShopService.Interfaces;
using ShopService.Models;

namespace ShopService.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly AppDbContext _context;

        public ShoppingCartRepository(AppDbContext context)
        {
            _context = context;
        }

        public Guid CreateShoppingCart()
        {   
            var shoppCart = new ShoppingCart();

            _context.ShoppingCarts.Add(shoppCart);
            _context.SaveChanges();

            return shoppCart.Id;
        }

        public ShoppingCart GetShoppingCartById(Guid id)
        {
            return _context.ShoppingCarts.FirstOrDefault(s => s.Id == id);
        }

        public bool UpdateShoppingCart(Guid IdShoppCart,Guid idProduct, decimal addedCost, string itemName, int addedQuantity)
        {
            var shoppCart = GetShoppingCartById(IdShoppCart);

            if(shoppCart != null)
            {
                // shoppCart.RefreshShoppingCart(addedCost, idProduct ,itemName, addedQuantity);
                _context.SaveChanges();

                return true;
            }

            return false;

        }
    }
}