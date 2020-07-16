using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderProduct = new HashSet<OrderProduct>();
        }

        public int Id { get; set; }
        public string OrderCode { get; set; }
        public int CafeTableId { get; set; }
        public DateTime AddedDate { get; set; }
        public DateTime LastUpdateDate { get; set; }
        public decimal Total { get; set; }

        public virtual CafeTable CafeTable { get; set; }
        public virtual ICollection<OrderProduct> OrderProduct { get; set; }
    }
}
