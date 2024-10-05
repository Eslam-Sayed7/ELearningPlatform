using System;
using System.Collections.Generic;

namespace Core.Entities;

public partial class CourseMaterial
{
    public int MaterialId { get; set; }
    public Guid CourseId { get; set; }
    public string FilePath { get; set; }

    public int Sequence { get; set; } 
    public virtual Course Course { get; set; }
}
