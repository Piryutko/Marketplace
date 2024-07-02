using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ShopService.Exceptions;
using ShopService.Interfaces;
using ShopService.Models;

namespace ShopService.Controllers
{
   [ApiController]
   [Route("api/[controller]")]
   public class ShopController : ControllerBase
   {
      private readonly IShopFacade _shopFacade;

      public ShopController(IShopFacade shopFacade)
      {
         _shopFacade = shopFacade;
      }

      [HttpGet("CheckNickname/{name}")] //http://localhost:7000/api/shop/CheckNickname/
      public ActionResult CheckNickname(string name)
      {
         try
         {
            var result = _shopFacade.CheckNickname(name);
            return Ok(result);
         }
         catch (UserNotFoundException)
         {
            return NotFound();
         }
         catch (GrpcServerUnavailableException)
         {
            return BadRequest();
         }
      }

      [HttpGet("GetItemsByCategory")] //http://localhost:7000/api/shop/GetItemsByCategory?Idcategory=
      public ActionResult<IEnumerable<Item>> GetItemsByCategory(int idCategory)
      {
         try
         {
            var result = _shopFacade.GetItemsByCategory(idCategory);
            return Ok(result);
         }
         catch (CategoryNotFoundException)
         {
            return NotFound();
         }
         catch (GrpcServerUnavailableException)
         {
            return BadRequest();
         }
      }

      [HttpGet("GetItemsCategorySortByCost")] //localhost:7000/api/shop/GetItemsCategorySortByCost?Idcategory=
      public ActionResult<IEnumerable<Item>> GetItemsCategorySortByCost(int categoryId)
      {
         try
         {
            var result = _shopFacade.GetItemsCategorySortByCost(categoryId);
            return Ok(result);
         }
         catch (GrpcServerUnavailableException)
         {
            return NotFound();
         }
      }

      [HttpGet("GetItemsCategorySortByCostDescending")] //http://localhost:7000/api/shop/GetItemsCategorySortByCostDescending?Idcategory=
      public ActionResult<IEnumerable<Item>> GetItemsCategorySortByCostDescending(int categoryId)
      {
         try
         {
            var result = _shopFacade.GetItemsCategorySortByCostDescending(categoryId);
            return Ok(result);
         }
         catch (CategoryNotFoundException)
         {
            return NotFound();
         }
         catch (GrpcServerUnavailableException)
         {
            return BadRequest();
         }

      }

      [HttpPost("CreateShoppCart/{itemId},{quantity}")] //http://localhost:7000/api/Shop/CreateShoppCart/{itemId},{quantity}
      public ActionResult CreateShoppCart(Guid itemId, int quantity)
      {
         try
         {
            var result = _shopFacade.CreateShoppCart(itemId, quantity);
            return Ok(result);
         }
         catch (ItemNotFoundException)
         {
            return BadRequest();
         }
         catch (GrpcServerUnavailableException)
         {
            return NotFound();
         }
      }

      [HttpPut("UpdateShoppingCart/shoppId={shoppId},itemId={itemId},quantity={quantity}")] //http://localhost:7000/api/Shop/UpdateShoppingCart/shoppId={shoppId},itemId={itemId},quantity={quantity}
      public ActionResult UpdateShoppingCart(Guid shoppId, Guid itemId, int quantity)
      {
         try
         {
            var result = _shopFacade.UpdateShoppingCart(shoppId, itemId, quantity);
            return Ok(result);
         }
         catch (GrpcServerUnavailableException)
         {
            return BadRequest();
         }
      }

      [HttpGet("GetShoppingCartById/{shoppId}")] //http://localhost:7000/api/Shop/GetShoppingCartById/{shoppId}
      public ActionResult GetShoppingCartById(Guid shoppId)
      {
         try
         {
            var result = _shopFacade.GetShoppingCartById(shoppId);
            return Ok(result);
         }
         catch (ShoppingCartNotFoundException)
         {
            return NotFound();
         }
         catch (GrpcServerUnavailableException)
         {
            return BadRequest();
         }
      }

      [HttpGet("GetAllProductsByShoppId/{shoppId}")] //http://localhost:7000/api/Shop/GetAllProductsByShoppId/{shoppId}
      public ActionResult GetAllProductsByShoppId(Guid shoppId)
      {
         try
         {
            var result = _shopFacade.GetAllProductsByShoppId(shoppId);
            return Ok(result);
         }
         catch (ShoppingCartNotFoundException)
         {
            return NotFound();
         }
         catch (GrpcServerUnavailableException)
         {
            return BadRequest();
         }
      }

      [HttpPut("UpdateProduct/shoppId={shoppId},productId={productId},quantity={quantity}")] //http://localhost:7000/api/Shop/UpdateProduct/shoppId={shoppId},productId={productId},quantity={quantity}
      public ActionResult TryUpdateProduct(Guid shoppId, Guid productId, int quantity)
      {
         try
         {
            var result = _shopFacade.TryUpdateProduct(shoppId, productId, quantity);
            return Ok(result);
         }
         catch (GrpcServerUnavailableException)
         {
            return BadRequest();
         }
      }


      [HttpDelete("DeleteProductsInShoppCart/shoppId={shoppId},productId={productId}")] //http://localhost:7000/api/Shop/DeleteProductsInShoppCart/shoppId={shoppId},productId={productId}
      public ActionResult DeleteProductsInShoppCart(Guid shoppId, Guid productId)
      {
         try
         {
            var result = _shopFacade.DeleteProductById(shoppId, productId);
            return Ok(result);

         }
         catch (GrpcServerUnavailableException)
         {
            return BadRequest();
         }
      }

      [HttpDelete("DeleteShoppCart/shoppId={shoppId}")] //http://localhost:7000/api/Shop/DeleteShoppCart/shoppId={shoppId}
      public ActionResult DeleteShoppCart(Guid shoppId)
      {
         try
         {
            var result = _shopFacade.DeleteShoppCart(shoppId);
            return Ok(result);

         }
         catch (GrpcServerUnavailableException)
         {
            return BadRequest();
         }
      }

      [HttpGet("CreateOrder/nickname={nickname},shoppid={shoppid}")] //http://localhost:7000/api/Shop/CreateOrder/nickname={nickname},shoppid={shoppid}
      public ActionResult CreateOrder(string nickname, Guid shoppId)
      {
         try
         {
            var result = _shopFacade.CreateOrder(nickname, shoppId);
            return Ok(result);
         }
         catch (UserNotFoundException)
         {
            return NotFound();
         }
         catch (GrpcServerUnavailableException)
         {
            return BadRequest();
         }
      }


   }
}