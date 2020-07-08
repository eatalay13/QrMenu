using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class ProductOption
    {
        public ProductOption()
        {
            ProductOptionValue = new HashSet<ProductOptionValue>();
        }

        public int Id { get; set; }
        public int ProductId { get; set; }
        public int OptionId { get; set; }
        public string Value { get; set; }
        public bool Required { get; set; }

        public virtual Option Option { get; set; }
        public virtual Product Product { get; set; }
        public virtual ICollection<ProductOptionValue> ProductOptionValue { get; set; }
    }
}
