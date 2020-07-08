using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class Option
    {
        public Option()
        {
            OptionValue = new HashSet<OptionValue>();
            ProductOption = new HashSet<ProductOption>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public short SortOrder { get; set; }

        public virtual ICollection<OptionValue> OptionValue { get; set; }
        public virtual ICollection<ProductOption> ProductOption { get; set; }
    }
}
