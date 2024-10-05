using Core.Enums;

namespace Core.Entities;

public class CourseSection
{
    public Guid ContentId { get; set; }
    public Guid CourseId { get; set; }
    public string? Title { get; set; }
    
    public int Sequence { get; set; } = 0; // used for ordering section contenet
    
    public CourseLevel level { get; set; }
    public DateTime? CreatedAt { get; set; }
    public virtual Course? Course { get; set; }
    public virtual ICollection<TextContent> TextContents { get; set; } = new List<TextContent>();
    public virtual ICollection<VideoContent> VideoContents { get; set; } = new List<VideoContent>();
}
