using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class CafeTable
    {
        public CafeTable()
        {
            Cart = new HashSet<Cart>();
            Order = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsBusy { get; set; }
        public int Number { get; set; }

        public virtual ICollection<Cart> Cart { get; set; }
        public virtual ICollection<Order> Order { get; set; }
    }
}
