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
        private readonly IItemRepository _itemRepository;
        private IShoppingCartRepository _shoppingCartRepository;

        //мне не нравится идея хранить товары в репозитории в этом сервисе
        //скорей всего они переедут сразу в Order


        public ShopController(IShopClient shopClient, IItemRepository itemRepository, IShoppingCartRepository shoppingCartRepository)
        {
            _shopClient = shopClient;
            _itemRepository = itemRepository;
            _shoppingCartRepository = shoppingCartRepository;
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

        [HttpGet("BuyItems/{itemId},{quantity}")]
        public ActionResult BuyItems(Guid itemId, int quantity)
        {
           var data = _shopClient.TryBuyItems(itemId, quantity, out decimal cost, out string itemName);

           if(data)
           {
                var shoppId = _shoppingCartRepository.CreateShoppingCart();
                var result = _shoppingCartRepository.UpdateShoppingCart(shoppId, cost, itemName, quantity);

                switch (result)
                {
                  case true: return Ok(shoppId);
                  case false: return Ok(result);
                }
           }

           return Ok(data);
        }

        [HttpPut("UpdateShoppingCart/shoppId={shoppId},itemId={itemId},quantity={quantity}")]
        public ActionResult UpdateShoppingCart(Guid shoppId, Guid itemId, int quantity)
        {
           var data = _shopClient.TryBuyItems(itemId, quantity, out decimal cost, out string itemName);

           if(data)
           {
                var result = _shoppingCartRepository.UpdateShoppingCart(shoppId, cost, itemName, quantity);
                
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



    }
}