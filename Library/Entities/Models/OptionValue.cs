using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class OptionValue
    {
        public OptionValue()
        {
            ProductOptionValue = new HashSet<ProductOptionValue>();
        }

        public int Id { get; set; }
        public int OptionId { get; set; }
        public string Image { get; set; }
        public short SortOrder { get; set; }
        public string Name { get; set; }

        public virtual Option Option { get; set; }
        public virtual ICollection<ProductOptionValue> ProductOptionValue { get; set; }
    }
}
