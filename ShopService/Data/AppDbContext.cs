using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShopService.Models;

namespace ShopService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext (DbContextOptions<AppDbContext> opt) : base(opt)
        {

        }

        public DbSet<User> Users { get; set; }

        public DbSet<Item> Items { get; set; }
        
        public DbSet<Order> Orders { get; set; }

        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
    }
}