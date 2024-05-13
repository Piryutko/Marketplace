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

        public List<Item> GetItems(int numberCategory)
        {
            _context.Items.Add(new Item {Name = "intel core i5", Category = Category.computers, Cost = 3400 });
            _context.Items.Add(new Item {Name = "intel core i7", Category = Category.computers, Cost = 3400 });
            _context.Items.Add(new Item {Name = "Adidas TShort", Category = Category.clothes, Cost = 100 });
            _context.Items.Add(new Item {Name = "intel core i5", Category = Category.computers, Cost = 3400 });
            _context.Items.Add(new Item {Name = "Chrysanthemums", Category = Category.flowers, Cost = 30 });

            var result = _context.Items.ToList().Where(i => i.Category == (Category)numberCategory).ToList();

            return result;
        }

        public List<Item> SortItemPrice(Category category)
        {
            throw new NotImplementedException();
        }
    }
}