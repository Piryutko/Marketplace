using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopService.Data;
using ShopService.Enums;
using ShopService.Interfaces;
using ShopService.Models;

namespace ShopService.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly AppDbContext _context;

        public ItemRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Item> GetItemsByCategory(int category)
        {
            var result = _context.Items.ToList().Where(i => i.Category == (Category)category);

            return result;
        }

        public IEnumerable<Item> SortItemPrice(int category)
        {
            var result = _context.Items.OrderBy(i => i.Cost);

            return result;
        }

        public IEnumerable<Item> SortItemPriceByDescending(int category)
        {
            var result = _context.Items.OrderByDescending(i => i.Cost);

            return result;

        }
    }
}