using System;
using System.Collections.Generic;

namespace Core.Entities;
public partial class Category
{
    public Guid CategoryId { get; set; } = Guid.NewGuid();

    public string CategoryName { get; set; } = null!;

    public Guid? ParentCategoryId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

    public virtual ICollection<Category> InverseParentCategory { get; set; } = new List<Category>();

    public virtual Category? ParentCategory { get; set; }
}
