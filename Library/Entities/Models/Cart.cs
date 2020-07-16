using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class Cart
    {
        public int Id { get; set; }
        public int CafeTableId { get; set; }
        public int ProductId { get; set; }
        public DateTime AddedDate { get; set; }

        public virtual CafeTable CafeTable { get; set; }
        public virtual Product Product { get; set; }
    }
}
