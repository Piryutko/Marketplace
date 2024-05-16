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
            var item = new Item {Name = "intel core i5", Category = Category.computers, Cost = 3400, Id = 1 };
            var item2 = new Item {Name = "intel core i5", Category = Category.computers, Cost = 3400, Id = 2 };

            _context.Items.Add(new Item {Name = "intel core i7", Category = Category.computers, Cost = 3400, Id = 3 });
            _context.Items.Add(new Item {Name = "Adidas TShort", Category = Category.clothes, Cost = 100, Id = 4 });
            _context.Items.Add(new Item {Name = "intel core i8", Category = Category.computers, Cost = 3400, Id = 5 });
            _context.Items.Add(new Item {Name = "Chrysanthemums", Category = Category.flowers, Cost = 30, Id = 6 });

            _context.Items.Add(item);
            _context.Items.Add(item2);


            _context.SaveChanges();

            //тут будет логика по работе с БД

            var result = _context.Items.ToList().Where(i => i.Category == (Category)category);

            return result;
        }

        public IEnumerable<Item> SortItemPrice(int category)
        {
            _context.Items.Add(new Item {Name = "intel core i7", Category = Category.computers, Cost = 3400, Id = 3 });
            _context.Items.Add(new Item {Name = "Adidas TShort", Category = Category.clothes, Cost = 100, Id = 4 });
            _context.Items.Add(new Item {Name = "intel core i8", Category = Category.computers, Cost = 3400, Id = 5 });
            _context.Items.Add(new Item {Name = "Chrysanthemums", Category = Category.flowers, Cost = 30, Id = 6 });
            _context.SaveChanges();

            //тут будет логика по работе с БД локально затем продуктивно
            var result = _context.Items.OrderBy(i => i.Cost);

            return result;
        }

        public IEnumerable<Item> SortItemPriceByDescending(int category)
        {
            _context.Items.Add(new Item {Name = "intel core i7", Category = Category.computers, Cost = 3400, Id = 3 });
            _context.Items.Add(new Item {Name = "Adidas TShort", Category = Category.clothes, Cost = 100, Id = 4 });
            _context.Items.Add(new Item {Name = "intel core i8", Category = Category.computers, Cost = 3400, Id = 5 });
            _context.Items.Add(new Item {Name = "Chrysanthemums", Category = Category.flowers, Cost = 30, Id = 6 });

            _context.SaveChanges();

            //тут будет логика по работе с БД локально затем продуктивно
            var result = _context.Items.OrderByDescending(i => i.Cost);

            return result;

        }
    }
}