using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public partial class ProductToCategory : IEntity
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
        public virtual Product Product { get; set; }
    }
}
