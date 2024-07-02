using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ItemService.Enums;
using ItemService.Exceptions;
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
            var result = _itemFacade.TryAddItem(item);

            return result ? Ok() : BadRequest();
        }

        [HttpGet("GetItemsByCategory")]
        public ActionResult GetItemsByCategory(Category category)
        {
            try
            {
                var result = _itemFacade.GetItemsByCategory(category);
                return Ok(result);
            }
            catch (CategoryNotFoundException)
            {
                return BadRequest();
            }
        }

        [HttpGet("GetItemsSortByCost")]
        public ActionResult GetItemsSortByCost(Category category)
        {
            try
            {
                var result = _itemFacade.GetItemsCategorySortByCost(category);
                return Ok(result);
            }
            catch (CategoryNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet("GetItemsCategorySortByCostDescending")]
        public ActionResult GetItemsCategorySortByCostDescending(Category category)
        {
            try
            {
                var result = _itemFacade.GetItemsCategorySortByCostDescending(category);
                return Ok(result);
            }
            catch (CategoryNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("TryDeleteItem/{id}")]
        public ActionResult TryDeleteItem(Guid id)
        {
            return _itemFacade.TryDeleteItem(id) ? Ok(true) : NotFound(false);
        }

        [HttpPut("CheckQuantityItem/{id},{value}")]
        public ActionResult CheckQuantityItem(Guid id, int value)
        {
            return _itemFacade.CheckQuantityItem(id, value) ? Ok(true) : NotFound(false);
        }







    }
}