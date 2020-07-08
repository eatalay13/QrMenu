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
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public virtual ICollection<ProductImage> ProductImage { get; set; }
    }
}
