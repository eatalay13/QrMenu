using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class ProductOptionValue
    {
        public int Id { get; set; }
        public int ProductOptionId { get; set; }
        public int OptionValueId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public virtual OptionValue OptionValue { get; set; }
        public virtual ProductOption ProductOption { get; set; }
    }
}
