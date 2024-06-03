using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ShopService.Models
{
    public class ShoppingCart
    {
        public ShoppingCart()
        {
            Id = Guid.NewGuid();
            Quantity = default;
            Cost = decimal.Zero;
            CreateProduct();
        }

        [Key]
        public Guid Id { get; set; }

        public List<Product> Products { get; set;} //Добавление новых айтемов\products* 

        public decimal Cost { get; set; } //это будет общей стоимостью корзины*

        public int Quantity { get; set; } //переедет в продукты


        public void RefreshShoppingCart(decimal addedCost, int addedQuantity)
        {
            AddCost(addedCost);
            AddQuantity(addedQuantity);
        }

        private void AddCost(decimal addedCost)
        {
            Cost += addedCost;
        }

        private void AddProduct(Product product)
        {
            if(product != null)
            {
                Products.Add(product);
                return;
            }
        }

        public void CreateProduct()
        {
            Products = new List<Product>();
        }

        private void AddQuantity(int addedQuantity)
        {
            Quantity += addedQuantity;
        }

        public List<Product> GetItemsList()
        {
            return Products;
        }
    }
}