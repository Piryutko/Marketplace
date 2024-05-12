using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShopService.Interfaces;

namespace ShopService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestApiController : ControllerBase
    {
        private readonly IShopClient _shopClient;

        public TestApiController(IShopClient shopClient)
        {
            _shopClient = shopClient;
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


    }
}