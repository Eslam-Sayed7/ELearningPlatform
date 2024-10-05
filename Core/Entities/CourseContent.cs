using System;
using System.Collections.Generic;

namespace Core.Entities;

public partial class CourseContent
{
    public Guid ContentId { get; set; }

    public Guid? CourseId { get; set; }

    public string? ContentType { get; set; }

    public string? Title { get; set; }

    public int? Sequence { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Course? Course { get; set; }

    public virtual ICollection<TextContent> TextContents { get; set; } = new List<TextContent>();

    public virtual ICollection<VideoContent> VideoContents { get; set; } = new List<VideoContent>();
}
