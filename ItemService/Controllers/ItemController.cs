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
        private readonly IItemFacade _itemFacade;

        public ItemController(IItemFacade itemFacade)
        {
            _itemFacade = itemFacade;
        }

        [HttpPost("TryAddItem")]
        public ActionResult TryAddItem(Item item)
        {
           return Ok(_itemFacade.TryAddItem(item));
        }

        [HttpGet("GetItemsByCategory")]
        public ActionResult GetItemsByCategory(Category category)
        {
            return Ok(_itemFacade.GetItemsByCategory(category));
        }

        [HttpGet("GetItemsSortByCost")] 
        public ActionResult GetItemsSortByCost(Category category)
        {
            return Ok(_itemFacade.GetItemsCategorySortByCost(category));
        }

        [HttpGet("GetItemsCategorySortByCostDescending")] 
        public ActionResult GetItemsCategorySortByCostDescending(Category category)
        {
            return Ok(_itemFacade.GetItemsCategorySortByCostDescending(category));
        }

        [HttpDelete("TryDeleteItem/{id}")]
        public ActionResult TryDeleteItem(Guid id)
        {
            return Ok(_itemFacade.TryDeleteItem(id));
        }

        [HttpPut("CheckQuantityItem/{id},{value}")] 
        public ActionResult CheckQuantityItem(Guid id,int value)
        {
            return Ok(_itemFacade.CheckQuantityItem(id,value));
        }







    }
}