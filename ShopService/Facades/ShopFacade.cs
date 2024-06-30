using System;
using System.Collections.Generic;
using System.Linq;
using ShopService.Interfaces;
using ShopService.Models;

namespace ShopService.Facades
{
    public class ShopFacade : IShopFacade
    {
        private readonly IShopClient _shopClient;
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;

        public ShopFacade(IShopClient shopClient,
        IShoppingCartRepository shoppingCartRepository,
        IProductRepository productRepository,
        IOrderRepository orderRepository)
        {

            _shopClient = shopClient;
            _shoppingCartRepository = shoppingCartRepository;
            _productRepository = productRepository;
            _orderRepository = orderRepository;

        }

        public bool CheckNickname(string name)
        {
            try
            {
                var response = _shopClient.GetResultRequestByNickname(name, out string result);

                if (response)
                {
                    return response;
                }

                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); //для анализа ошибок будет добавлен Serilog
                return false;
            }
        }

        public IEnumerable<Item> GetItemsByCategory(int categoryId)
        {
            try
            {
                var data = _shopClient.GetItemsByCategory(categoryId);

                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error - {ex.Message}"); //для анализа ошибок будет добавлен Serilog
                var emptyData = new List<Item>();

                return emptyData;
            }
        }

        public IEnumerable<Item> GetItemsCategorySortByCost(int categoryId)
        {
            try
            {
                return _shopClient.GetItemsCategorySortByCost(categoryId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error - {ex.Message}"); //для анализа ошибок будет добавлен Serilog
                var emptyData = new List<Item>();

                return emptyData;
            }

        }
    }
}