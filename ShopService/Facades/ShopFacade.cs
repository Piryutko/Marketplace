using System;
using System.Collections.Generic;
using System.Linq;
using Grpc.Core;
using Microsoft.AspNetCore.Server.IIS.Core;
using ShopService.Exceptions;
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

        private const string MESSAGE_ERROR = "Возникла ошибка, обратитесь в службу поддержки";
        private const string STATUS_ERROR = "Ошибка";
        private const string MESSAGE_EMPTY_ITEM = "Товар не найден";

        private const string MESSAGE_EMPTY_USERNAME = "Пользователь не зарегистрирован в системе";

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

                return response ? true : throw new UserNotFoundException();
            }
            catch (RpcException)
            {
                throw new GrpcServerUnavailableException();
            }
        }

        public Response CreateShoppCart(Guid itemId, int quantity)
        {
            try
            {
                var data = _shopClient.TryAddItemInShoppCart(itemId, quantity, out decimal cost, out string itemName);

                if (data)
                {
                    var shoppId = _shoppingCartRepository.CreateShoppingCart();

                    var productId = _productRepository.CreateProduct(shoppId, itemId, itemName, cost, quantity);

                    var result = _shoppingCartRepository.UpdateShoppingCart(shoppId, cost * quantity, quantity);

                    switch (result)
                    {
                        case true:
                            return
                         new Response { Message = $"Индентификатор корзины - {shoppId}", Status = result.ToString() };
                        case false:
                            return
                         new Response { Status = result.ToString() };
                    }
                }

                throw new ItemNotFoundException();

            }
            catch (RpcException)
            {
                throw new GrpcServerUnavailableException();
            }

        }

        public IEnumerable<Item> GetItemsByCategory(int categoryId)
        {
            try
            {
                var data = _shopClient.GetItemsByCategory(categoryId);

                return data.Any() ? data : throw new CategoryNotFoundException();

            }
            catch (RpcException)
            {
                throw new GrpcServerUnavailableException();
            }
        }

        public IEnumerable<Item> GetItemsCategorySortByCost(int categoryId)
        {
            try
            {
                var result = _shopClient.GetItemsCategorySortByCost(categoryId);

                return result.Any() ? result : throw new CategoryNotFoundException();
            }
            catch (RpcException)
            {
                throw new GrpcServerUnavailableException();
            }

        }

        public IEnumerable<Item> GetItemsCategorySortByCostDescending(int categoryId)
        {
            try
            {
                var data = _shopClient.GetItemsCategorySortByCostDescending(categoryId);

                return data.Any() ? data : throw new CategoryNotFoundException();
            }
            catch (RpcException)
            {
                throw new GrpcServerUnavailableException();
            }
        }

        public bool UpdateShoppingCart(Guid shoppId, Guid itemId, int quantity)
        {
            try
            {
                var checkedData = _shopClient.CheckQuantityItem(itemId, quantity, out decimal cost, out string itemName);

                if (checkedData)
                {
                    var productId = _productRepository.CreateProduct(shoppId, itemId, itemName, cost, quantity);

                    var result = _shoppingCartRepository.UpdateShoppingCart(shoppId, cost * quantity, quantity);

                    if (result)
                    {
                        return result;
                    }

                    return result;
                }

                return checkedData;

            }
            catch (RpcException)
            {
                throw new GrpcServerUnavailableException();
            }
        }

        public ShoppingCart GetShoppingCartById(Guid id)
        {
            try
            {
                var result = _shoppingCartRepository.GetShoppingCartById(id);

                if (result != null)
                {
                    return result;
                }

                throw new ShoppingCartNotFoundException();
            }
            catch (RpcException)
            {
                throw new GrpcServerUnavailableException();
            }

        }

        public IEnumerable<Product> GetAllProductsByShoppId(Guid shoppId)
        {
            try
            {
                var result = _productRepository.GetAllProductsByShoppId(shoppId);
                return result.Any() ? result : throw new ShoppingCartNotFoundException();
            }
            catch (RpcException)
            {
                throw new GrpcServerUnavailableException();
            }
        }

        public bool TryUpdateProduct(Guid shoppId, Guid productId, int quantity)
        {
            try
            {
                var result = _productRepository.TryUpdateProduct(shoppId, productId, quantity);

                if (result)
                {
                    var products = _productRepository.GetAllProductsByShoppId(shoppId);

                    decimal cost = default;
                    int sumProducts = default;

                    foreach (var product in products)
                    {
                        _productRepository.ModifySumProductValue(product.Quantity, ref sumProducts);
                        _productRepository.ModifyCostProductValue(product.Cost, ref cost);
                    }

                    _shoppingCartRepository.RefreshShoppingCart(shoppId, sumProducts, cost);

                    return result;
                }

                return result;

            }
            catch (RpcException)
            {
                throw new GrpcServerUnavailableException();
            }
        }

        public bool DeleteProductById(Guid shoppId, Guid productId)
        {
            try
            {
                var result = _productRepository.DeleteProductById(productId);

                if (result)
                {
                    var products = _productRepository.GetAllProductsByShoppId(shoppId);

                    decimal cost = default;
                    int sumProducts = default;

                    foreach (var product in products)
                    {
                        _productRepository.ModifySumProductValue(product.Quantity, ref sumProducts);
                        _productRepository.ModifyCostProductValue(product.Cost, ref cost);
                    }

                    _shoppingCartRepository.RefreshShoppingCart(shoppId, sumProducts, cost);

                    return result;
                }

                return result;

            }
            catch (RpcException)
            {
                throw new GrpcServerUnavailableException();
            }
        }

        public bool DeleteShoppCart(Guid shoppId)
        {
            try
            {
                _shoppingCartRepository.DeleteShoppingCart(shoppId);
                _productRepository.DeleteProductsByShoppId(shoppId);

                if (_shoppingCartRepository.DeleteShoppingCart(shoppId) && _productRepository.DeleteProductsByShoppId(shoppId))
                {
                    return true;
                }

                return false;

            }
            catch (RpcException)
            {
                throw new GrpcServerUnavailableException();
            }
        }

        public Order CreateOrder(string nickname, Guid shoppId)
        {
            try
            {
                int sumProducts = default;
                decimal cost = default;
                string orderInfo = default;

                _shopClient.GetResultRequestByNickname(nickname, out string result);

                if (bool.Parse(result) == true)
                {
                    var products = _productRepository.GetAllProductsByShoppId(shoppId);

                    if (!products.Any())
                    {
                        throw new ShoppingCartNotFoundException();
                    }

                    foreach (var product in products)
                    {
                        if (_shopClient.BuyItem(product.ItemId, product.Quantity))
                        {
                            _productRepository.ModifySumProductValue(product.Quantity, ref sumProducts);
                            _productRepository.ModifyCostProductValue(product.Cost, ref cost);
                        }
                        else
                        {
                            if (orderInfo == default)
                            {
                                orderInfo = $"Нужное Вам количество товаров отсутвует на складе, данные товары не будут добавлены в ваш заказ {product.ItemId} ";
                            }
                            else
                            {
                                orderInfo += $",{product.ItemId}";
                            }
                        }
                    }

                    if (orderInfo == default)
                    {
                        orderInfo = "Все товары успешно добавлены в ваш заказ";
                    }

                    var order = _orderRepository.CreateOrder(new Order(nickname, shoppId, sumProducts, cost));

                    if (order != null)
                    {
                        order.AddInfo(orderInfo);
                        return order;
                    }

                }

                throw new UserNotFoundException();
            }
            catch (RpcException)
            {
                throw new GrpcServerUnavailableException();
            }
        }
    }
}