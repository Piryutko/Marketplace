using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopService.Enums;

namespace ShopService.Models
{
    public class Item
    {
        public string Name { get; set; }
        
        public Category Category { get; set; }

        public decimal Cost {get; set; }
        
    }
}