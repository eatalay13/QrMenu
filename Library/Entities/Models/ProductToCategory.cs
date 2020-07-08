using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class ProductToCategory
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
        public virtual Product Product { get; set; }
    }
}
