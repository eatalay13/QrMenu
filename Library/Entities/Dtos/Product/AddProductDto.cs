using Entities.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.Product
{
    public class AddProductDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
        public List<int> CategoryIds { get; set; }
        public string MainPhoto { get; set; }
        public List<ProductImage> Photos { get; set; }
    }
}
