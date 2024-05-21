using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ItemService.Enums;
using ItemService.Interfaces;
using ItemService.Models;
using Microsoft.AspNetCore.Mvc;

namespace ItemService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly IItemRepository _itemRepository;

        public ItemController(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        [HttpPost("TryAddItem")]
        public ActionResult TryAddItem(Item item)
        {
           if(_itemRepository.TryAddItem(item))
           {
            return Ok(true);
           }
           return BadRequest(false);
        }

        [HttpGet("GetItemsByCategory")]
        public ActionResult GetItemsByCategory(Category category)
        {
            return Ok(_itemRepository.GetItemsByCategory(category));
        }






    }
}