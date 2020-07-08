using Core.Entities;
using System;
using System.Collections.Generic;

namespace Entities.Models
{
    public partial class Category : IEntity
    {
        public Category()
        {
            InverseParentCategory = new HashSet<Category>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentCategoryId { get; set; }
        public int SortOrder { get; set; }
        public string Description { get; set; }

        public virtual Category ParentCategory { get; set; }
        public virtual ICollection<Category> InverseParentCategory { get; set; }
    }
}
