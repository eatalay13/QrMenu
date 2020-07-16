using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class Product
    {
        public Product()
        {
            Cart = new HashSet<Cart>();
            ProductImage = new HashSet<ProductImage>();
            ProductToCategory = new HashSet<ProductToCategory>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<Cart> Cart { get; set; }
        public virtual ICollection<ProductImage> ProductImage { get; set; }
        public virtual ICollection<ProductToCategory> ProductToCategory { get; set; }
    }
}
