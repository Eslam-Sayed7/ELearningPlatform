using System;
using System.Collections.Generic;

namespace Core.Entities;

public partial class VideoContent
{
    public int VideoId { get; set; }

    public int? ContentId { get; set; }

    public string? VideoUrl { get; set; }

    public int? Duration { get; set; }

    public virtual CourseContent? Content { get; set; }
}
