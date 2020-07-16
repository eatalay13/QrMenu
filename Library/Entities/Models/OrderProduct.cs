using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class OrderProduct
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public short Count { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Order Order { get; set; }
    }
}
