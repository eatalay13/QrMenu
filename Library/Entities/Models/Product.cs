using Core.Entities;
using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class Product : IEntity
    {
        public Product()
        {
            ProductImage = new HashSet<ProductImage>();
            ProductOption = new HashSet<ProductOption>();
        }

        public int Id { get; set; }
        public string StockCode { get; set; }
        public string Name { get; set; }
        public string Barcode { get; set; }
        public string Model { get; set; }
        public int Quantity { get; set; }
        public string Image { get; set; }
        public int? TaxClassId { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public virtual TaxClass TaxClass { get; set; }
        public virtual ICollection<ProductImage> ProductImage { get; set; }
        public virtual ICollection<ProductOption> ProductOption { get; set; }
    }
}
