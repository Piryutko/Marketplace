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

      private const string MESSAGE_ERROR = "Возникла ошибка, обратитесь в службу поддержки";
      private const string STATUS_ERROR = "Ошибка";

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
         try
         {
            var response = _shopClient.GetResultRequestByNickname(name, out string result);

            if (response == true)
            {
               return Ok(result);
            }

            return BadRequest();

         }
         catch
         {
            return BadRequest(new Response() { Message = MESSAGE_ERROR, Status = STATUS_ERROR });
         }

      }

      [HttpGet("GettAllItems")]
      public ActionResult<IEnumerable<Item>> GetAllItems(int idCategory)
      {
         try
         {
            var data = _shopClient.GetItemsByCategory(idCategory);

            return Ok(data);
         }
         catch
         {
            return BadRequest(new Response() { Message = MESSAGE_ERROR, Status = STATUS_ERROR });
         }

      }

      [HttpGet("GetItemsCategorySortByCost")]
      public ActionResult<IEnumerable<Item>> GetItemsCategorySortByCost(int idCategory)
      {
         try
         {
            var data = _shopClient.GetItemsCategorySortByCost(idCategory);

            return Ok(data);

         }
         catch
         {
            return BadRequest(new Response() { Message = MESSAGE_ERROR, Status = STATUS_ERROR });
         }
      }

      [HttpGet("GetItemsCategorySortByCostDescending")]
      public ActionResult<IEnumerable<Item>> GetItemsCategorySortByCostDescending(int idCategory)
      {
         try
         {
            var data = _shopClient.GetItemsCategorySortByCostDescending(idCategory);

            return Ok(data);
         }
         catch
         {
            return BadRequest(new Response() { Message = MESSAGE_ERROR, Status = STATUS_ERROR });
         }

      }

      [HttpPost("TryAddItemInNewShoppCart/{itemId},{quantity}")]
      public ActionResult TryAddItemInNewShoppCart(Guid itemId, int quantity)
      {
         try
         {
            var data = _shopClient.TryAddItemInShoppCart(itemId, quantity, out decimal cost, out string itemName);

            if (data)
            {
               var shoppId = _shoppingCartRepository.CreateShoppingCart();

               var productId = _productRepository.CreateProduct(shoppId, itemId, itemName, cost, quantity);

               var result = _shoppingCartRepository.UpdateShoppingCart(shoppId, cost * quantity, quantity);

               var itemtest = _shoppingCartRepository.GetShoppingCartById(shoppId);

               switch (result)
               {
                  case true:
                     return Ok
                  (new Response { Message = $"Индентификатор корзины - {shoppId}", Status = result.ToString() });
                  case false:
                     return Ok
                  (new Response { Status = result.ToString() });
               }
            }

            return Ok(data);

         }
         catch
         {
            return BadRequest(new Response() { Message = MESSAGE_ERROR, Status = STATUS_ERROR });
         }

      }

      [HttpPut("UpdateShoppingCart/shoppId={shoppId},itemId={itemId},quantity={quantity}")]
      public ActionResult UpdateShoppingCart(Guid shoppId, Guid itemId, int quantity)
      {
         try
         {
            var data = _shopClient.CheckQuantityItem(itemId, quantity, out decimal cost, out string itemName);

            if (data)
            {
               var productId = _productRepository.CreateProduct(shoppId, itemId, itemName, cost, quantity);

               var result = _shoppingCartRepository.UpdateShoppingCart(shoppId, cost * quantity, quantity);

               if (result)
               {
                  return Ok(_shoppingCartRepository.GetShoppingCartById(shoppId));
               }

               return Ok(result);
            }

            return Ok(data);

         }
         catch
         {
            return BadRequest(new Response() { Message = MESSAGE_ERROR, Status = STATUS_ERROR });
         }

      }

      [HttpGet("GetShoppingCartById/{shoppId}")]
      public ActionResult GetShoppingCartById(Guid shoppId)
      {
         try
         {
            return Ok(_shoppingCartRepository.GetShoppingCartById(shoppId));
         }
         catch
         {
            return BadRequest(new Response() { Message = MESSAGE_ERROR, Status = STATUS_ERROR });
         }
      }

      [HttpGet("GetAllProductsByShoppId/{shoppId}")]
      public ActionResult GetAllProductsByShoppId(Guid shoppId)
      {
         try
         {
            var products = _productRepository.GetAllProductsByShoppId(shoppId);

            return Ok(products);
         }
         catch
         {
            return BadRequest(new Response() { Message = MESSAGE_ERROR, Status = STATUS_ERROR });
         }

      }

      [HttpPut("UpdateProduct/shoppId={shoppId},productId={productId},quantity={quantity}")]
      public ActionResult TryUpdateProduct(Guid shoppId, Guid productId, int quantity)
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

               return Ok(result);
            }

            return Ok(result);

         }
         catch
         {
            return BadRequest(new Response() { Message = MESSAGE_ERROR, Status = STATUS_ERROR });
         }

      }


      [HttpDelete("DeleteProductsInShoppCart/shoppId={shoppId},productId={productId}")]
      public ActionResult DeleteProductsInShoppCart(Guid shoppId, Guid productId)
      {
         try
         {
            _productRepository.DeleteProductById(productId);

            if (true) // доделать*
            {
               var products = _productRepository.GetAllProductsByShoppId(shoppId);

               decimal cost = default;
               int sumProducts = default;

               foreach (var product in products) //перенести в метод*
               {
                  _productRepository.ModifySumProductValue(product.Quantity, ref sumProducts);
                  _productRepository.ModifyCostProductValue(product.Cost, ref cost);
               }

               _shoppingCartRepository.RefreshShoppingCart(shoppId, sumProducts, cost);

               return Ok(true);
            }

         }
         catch
         {
            return BadRequest(new Response() { Message = MESSAGE_ERROR, Status = STATUS_ERROR });
         }


      }

      [HttpDelete("DeleteShoppCart/shoppId={shoppId}")]
      public ActionResult DeleteShoppCart(Guid shoppId)
      {
         try
         {
            _shoppingCartRepository.DeleteShoppingCart(shoppId);
            _productRepository.DeleteProductsByShoppId(shoppId);

            return Ok();

         }
         catch
         {
            return BadRequest(new Response() { Message = MESSAGE_ERROR, Status = STATUS_ERROR });
         }
      }

      [HttpGet("CreateOrder/nickname={nickname}, shoppid={shoppid}")]
      public ActionResult CreateOrder(string nickname, Guid shoppId)
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
                  return Ok(order);
               }

            }
            return Ok($"Пользователь {nickname} - не зарегистрирован в магазине");
         }
         catch
         {
            return BadRequest(new Response() { Message = MESSAGE_ERROR, Status = STATUS_ERROR });
         }

      }


   }
}