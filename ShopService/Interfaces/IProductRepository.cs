using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopService.Models;

namespace ShopService.Interfaces
{
    public interface IProductRepository
    {
        Guid CreateProduct(Guid shoppId, Guid itemId, string name, decimal cost, int quantity);

        List<Product> GetAllProducts();

        IEnumerable<Product> GetAllProductsByShoppId(Guid shoppId);

        bool TryUpdateProduct(Guid shoppId, Guid productId, int quantity);

        bool DeleteProductsByShoppId(Guid id);

        bool DeleteProductById(Guid id);

        Product GetProductByShoppIdProductId(Guid shoppId, Guid productId);

        void ModifySumProductValue(int quantity, ref int sumProducts);

        void ModifyCostProductValue(decimal cost, ref decimal sumCost);
    }
}