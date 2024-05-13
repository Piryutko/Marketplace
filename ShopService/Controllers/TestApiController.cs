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
    public class TestApiController : ControllerBase
    {
        private readonly IShopClient _shopClient;
        private readonly IItemRepository _itemRepository;

        public TestApiController(IShopClient shopClient, IItemRepository itemRepository)
        {
            _shopClient = shopClient;
            _itemRepository = itemRepository;
        }

        [HttpGet("test/{name}")]
        public ActionResult TestRequest(string name)
        {
           var testResult = _shopClient.GetResultRequestByNickname(name, out string result);

           if (testResult == true)
           {
                return Ok(result);
           }

           return BadRequest();
        }

        [HttpGet("testRequestAllItems/{id}")]
        public ActionResult<List<Item>> TestRequestAllItems(int id)
        {
           var result = _itemRepository.GetItems(id);

           return Ok(result);
        }



    }
}