using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ItemService.Data;
using ItemService.Enums;
using ItemService.Interfaces;
using ItemService.Models;

namespace ItemService.Repository
{
    public class ItemRepository : IItemRepository
    {
        private readonly AppDbContext _context;

        public ItemRepository(AppDbContext context)
        {
            _context = context;
        }
        public bool BuyItem(Guid id, int value)
        {
            var item = _context.Items.FirstOrDefault(i => i.Id == id);
            var result = item.TryReduceQuantity(value);
            
            _context.SaveChanges();
            return result;
        }

        public IEnumerable<Item> GetItemsByCategory(Category category)
        {
            var test = _context.Items.Where(i => i.Category == category);
            return test;
        }

        public IEnumerable<Item> GetItemsCategorySortByCost(Category category)
        {
            return _context.Items.Where(i => i.Category == category).OrderBy(i => i.Cost);
        }

        public IEnumerable<Item> GetItemsCategorySortByCostDescending(Category category)
        {
            return _context.Items.OrderByDescending(i => i.Cost);
        }

        public bool TryAddItem(Item item)
        {
            try
            {
            _context.Items.Add(item);
            _context.SaveChanges();
            return true;
            }
            catch (System.Exception ex)
            {
            Console.WriteLine(ex.Message);
            return false;
            }
        }

        public bool TryDeleteItem(Guid id)
        {
            var result = _context.Items.Any(i => i.Id == id);

            if(result == true)
            {
                var item = _context.Items.FirstOrDefault(i => i.Id == id);

                _context.Items.Remove(item);
                _context.SaveChanges();
                return true;
            }

            return false;
        }
    }
}