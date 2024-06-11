using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShopService.Interfaces;
using ShopService.Models;
using ShopService.Repositories;

namespace ShopService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShopController : ControllerBase
    {
        private readonly IShopClient _shopClient;
        private IShoppingCartRepository _shoppingCartRepository;
        private IProductRepository _productRepository;

        private IOrderRepository _orderRepository;

        public ShopController(IShopClient shopClient, IItemRepository itemRepository,
         IShoppingCartRepository shoppingCartRepository, IProductRepository productRepository, IOrderRepository orderRepository)
        {
            _shopClient = shopClient;
            _shoppingCartRepository = shoppingCartRepository;
            _productRepository = productRepository;
            _orderRepository = orderRepository;
        }

        [HttpGet("CheckNickname/{name}")]
        public ActionResult CheckNickname(string name)
        {
           var response = _shopClient.GetResultRequestByNickname(name, out string result);

           if (response == true)
           {
               return Ok(result);
           }

           return BadRequest();
        }

        [HttpGet("GettAllItems")]
        public ActionResult<IEnumerable<Item>> GetAllItems(int idCategory)
        {
           var data = _shopClient.GetItemsByCategory(idCategory);

           return Ok(data);
        }

        [HttpGet("GetItemsCategorySortByCost")]
        public ActionResult<IEnumerable<Item>> GetItemsCategorySortByCost(int idCategory)
        {
           var data = _shopClient.GetItemsCategorySortByCost(idCategory);

           return Ok(data);
        }

        [HttpGet("GetItemsCategorySortByCostDescending")]
        public ActionResult<IEnumerable<Item>> GetItemsCategorySortByCostDescending(int idCategory)
        {
           var data = _shopClient.GetItemsCategorySortByCostDescending(idCategory);

           return Ok(data);
        }

        [HttpPost("TryAddItemInNewShoppCart/{itemId},{quantity}")]
        public ActionResult TryAddItemInNewShoppCart(Guid itemId, int quantity)
        {
           var data = _shopClient.TryAddItemInShoppCart(itemId, quantity, out decimal cost, out string itemName);

           if(data)
           {
                var shoppId = _shoppingCartRepository.CreateShoppingCart();

                var productId = _productRepository.CreateProduct(shoppId, itemId, itemName, cost,quantity);

                var result = _shoppingCartRepository.UpdateShoppingCart(shoppId, cost * quantity, quantity);

                var itemtest = _shoppingCartRepository.GetShoppingCartById(shoppId);

                switch (result)
                {
                  case true: return Ok
                  (new Response {Message = $"Индентификатор корзины - {shoppId}", Status = result.ToString()});
                  case false: return Ok
                  (new Response {Status = result.ToString()});
                }
           }

           return Ok(data);
        }

        [HttpPut("UpdateShoppingCart/shoppId={shoppId},itemId={itemId},quantity={quantity}")]
        public ActionResult UpdateShoppingCart(Guid shoppId, Guid itemId, int quantity)
        {
           var data = _shopClient.CheckQuantityItem(itemId, quantity, out decimal cost, out string itemName);

           if(data)
           {
               var productId = _productRepository.CreateProduct(shoppId, itemId, itemName, cost,quantity);

               var result = _shoppingCartRepository.UpdateShoppingCart(shoppId, cost * quantity, quantity);

                if(result)
                {
                  return Ok(_shoppingCartRepository.GetShoppingCartById(shoppId));
                }

                return Ok(result);
           }

           return Ok(data);
        }

        [HttpGet("GetShoppingCartById/{shoppId}")]
        public ActionResult GetShoppingCartById(Guid shoppId)
        {

         return Ok(_shoppingCartRepository.GetShoppingCartById(shoppId));

        }

        [HttpGet("GetAllProductsByShoppId/{shoppId}")]
        public ActionResult GetAllProductsByShoppId(Guid shoppId)
        {
            var products = _productRepository.GetAllProductsByShoppId(shoppId);

            return Ok(products);
        }

         [HttpPut("UpdateProduct/shoppId={shoppId},productId={productId},quantity={quantity}")]
         public ActionResult TryUpdateProduct(Guid shoppId, Guid productId, int quantity)
         {
            var result = _productRepository.TryUpdateProduct(shoppId, productId, quantity);

            if(result)
            {
               var products = _productRepository.GetAllProductsByShoppId(shoppId);

               decimal cost = default;
               int quantityCount = default;

               foreach (var product in products)
               {
                  cost += product.Cost;
                  quantityCount += product.Quantity;
               }

               _shoppingCartRepository.RefreshShoppingCart(shoppId, quantityCount, cost);
               
               return Ok(result); //Рефакторинг
            }
            
            return Ok(result);
         }


         [HttpDelete("DeleteProductsInShoppCart/shoppId={shoppId},productId={productId}")]
         public ActionResult DeleteProductsInShoppCart(Guid shoppId, Guid productId)
         {
            _productRepository.DeleteProductById(productId);

            if(true)
            {
               var products = _productRepository.GetAllProductsByShoppId(shoppId);

               decimal cost = default;
               int quantityCount = default;

               foreach (var product in products)
               {
                  cost += product.Cost;
                  quantityCount += product.Quantity;
               }

               _shoppingCartRepository.RefreshShoppingCart(shoppId, quantityCount, cost);
               
               return Ok(true); //Рефакторинг
            }


         }

         [HttpDelete("DeleteShoppCart/shoppId={shoppId}")]
         public ActionResult DeleteShoppCart(Guid shoppId)
         {
            _shoppingCartRepository.DeleteShoppingCart(shoppId);
            _productRepository.DeleteProductsByShoppId(shoppId);

            return Ok();
         }

         [HttpGet("CreateOrder/nickname={nickname}, shoppid={shoppid}")]
         public ActionResult CreateOrder(string nickname, Guid shoppId)
         {
            // _shopClient.GetResultRequestByNickname(nickname, out string result);

            // if(bool.Parse(result))
            {
               var products = _productRepository.GetAllProductsByShoppId(shoppId);
               //я буду отправлять по одному айдишнику на сервер для его проверки
               //дальше необходимо будет разработать функционал по передаче IdProduct и его проверке
               var product = products.FirstOrDefault();
               var productId = product.ItemId;
               var quantity = product.Quantity;

               var response = _shopClient.BuyItem(productId, quantity);
               var quantityProducts = product.Quantity;
               var cost = product.Cost;

               if(response)
               {

                  var order = _orderRepository.CreateOrder("Test", shoppId, quantityProducts, cost);
                  return Ok(order);

               }
               return Ok(false);
            }

            // return Ok(new Response(){Message = "Пользователь не найден", Status = "False"});
         }


    }
}