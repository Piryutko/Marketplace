using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopService.Data;
using ShopService.Interfaces;
using ShopService.Models;

namespace ShopService.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }
        public Guid CreateProduct(Guid shoppId, Guid itemId, string name, decimal cost, int quantity)
        {
            var product = new Product(shoppId, itemId, name, cost, quantity); 
            _context.Products.Add(product);
            _context.SaveChanges();

            return product.ProductId;
        }

        public List<Product> GetAllProducts()
        {
            return _context.Products.ToList();
        }

        public List<Product> GetAllProductsByShoppId(Guid shoppId)
        {
            var products = _context.Products.Where(i => i.ShoppId == shoppId);

            return products.ToList();
        }
    }
}