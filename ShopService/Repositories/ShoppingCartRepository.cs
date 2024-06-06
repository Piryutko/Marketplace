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

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public ShoppingCart GetShoppingCartById(Guid id)
        {
            return _context.ShoppingCarts.FirstOrDefault(s => s.Id == id);
        }

        public bool UpdateShoppingCart(Guid shoppCartId, decimal addedCost, int addedQuantity)
        {
            var shoppCart = GetShoppingCartById(shoppCartId);

            if(shoppCart != null)
            {
                shoppCart.UpdateShoppingCart(addedCost, addedQuantity);
                _context.SaveChanges();
                return true;
            }

            return false;

        }

        public void RefreshShoppingCart(Guid shoppCartId, int quantity, decimal cost)
        {
            var shoppCart = GetShoppingCartById(shoppCartId);
            
            if(shoppCart != null)
            {
                shoppCart.RefreshShoppingCart(quantity, cost);
                _context.SaveChanges();
            }
        }
    }
}