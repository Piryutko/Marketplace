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

        List<Product> GetAllProductsByShoppId(Guid shoppId);
    }
}