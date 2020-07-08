using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class TaxClass
    {
        public TaxClass()
        {
            Product = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? Rate { get; set; }

        public virtual ICollection<Product> Product { get; set; }
    }
}
