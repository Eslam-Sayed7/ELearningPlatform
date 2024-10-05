using System;
using System.Collections.Generic;

namespace Core.Entities;

public partial class CourseMaterial
{
    public int MaterialId { get; set; }

    public int? CourseId { get; set; }

    public string? FilePath { get; set; }

    public string? FileType { get; set; }

    public string? Description { get; set; }

    public int? Sequence { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Course? Course { get; set; }
}
