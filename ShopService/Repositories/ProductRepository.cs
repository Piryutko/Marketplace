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

            product.ChangeCost(quantity);
            
            _context.Products.Add(product);

            _context.SaveChanges();

            return product.ProductId;
        }

        public List<Product> GetAllProducts()
        {
            return _context.Products.ToList();
        }

        public IEnumerable<Product> GetAllProductsByShoppId(Guid shoppId)
        {
            var products = _context.Products.Where(i => i.ShoppId == shoppId);

            return products;
        }

        public bool TryUpdateProduct(Guid shoppId, Guid productId, int quantity)
        {
            var product = GetProductByShoppIdProductId(shoppId, productId);

            if(product != null)
            {
                product.ChangeQuantity(quantity);
                product.ChangeCost(quantity);
                _context.SaveChanges();
                return true;
            }

            return false;
        }

        public void DeleteProductsByShoppId(Guid id)
        {
            var products = _context.Products.Where(p => p.ShoppId == id);
            var result = _context.Products.Except(products);
            _context.SaveChanges();
        }

        public void DeleteProductById(Guid id)
        {
            var product = _context.Products.FirstOrDefault(p => p.ProductId == id);
            var result = _context.Products.Remove(product);
            _context.SaveChanges();
        }

        public Product GetProductByShoppIdProductId(Guid shoppId, Guid productId)
        {
            var product = _context.Products.FirstOrDefault(p => p.ShoppId == shoppId && p.ProductId == productId);

            return product;
        }

        public void ModifySumProductValue(int quantity, ref int value)
        {
            value += quantity;
        }

        public void ModifyCostProductValue(decimal cost, ref decimal sumCost)
        {
            sumCost += cost;
        }
    }
}