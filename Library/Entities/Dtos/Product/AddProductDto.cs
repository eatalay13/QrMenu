using Entities.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dtos.Product
{
    public class AddProductDto
    {
        public int Id { get; set; }
        public string StockCode { get; set; }
        public string Name { get; set; }
        public string Barcode { get; set; }
        public string Model { get; set; }
        public int Quantity { get; set; }
        public int? TaxClassId { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public List<int> CategoryIds { get; set; }
        public string MainPhoto { get; set; }
        public List<ProductImage> Photos { get; set; }
    }
}
