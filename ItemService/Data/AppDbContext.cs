using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ItemService.Models;
using Microsoft.EntityFrameworkCore;

namespace ItemService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {
            
        }

        public DbSet<Item> Items { get; set; }
    }
}