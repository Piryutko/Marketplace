using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ShopService.Models
{
    public class Product
    {
        public Product(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public Guid? Id { get; set; }

        public string? Name { get; set; }
        
        public decimal Price { get; set; }

    }
}