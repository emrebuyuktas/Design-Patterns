﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace DecoratorPattern.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public int Stock { get; set; }
        public string UserId { get; set; }
    }
}