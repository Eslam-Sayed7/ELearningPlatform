using System;
using System.Collections.Generic;

namespace Core.Entities;

public partial class TextContent
{
    public Guid TextId { get; set; }

    public Guid? ContentId { get; set; }

    public string? Body { get; set; }

    public virtual CourseContent? Content { get; set; }
}
