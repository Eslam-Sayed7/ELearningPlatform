using Core.Enums;

namespace Core.Entities;

public class CourseSection
{
    public Guid SectionId { get; set; } = Guid.NewGuid();
    public Guid CourseId { get; set; }
    public string? Title { get; set; }
    public int SectionSequence { get; set; } = 0;           // used for ordering section materials
    public int LastMaterialSequence { get; set; } = 0;
    public virtual Course? Course { get; set; }
    public virtual ICollection<CourseMaterial> CourseMaterials { get; set; } = new List<CourseMaterial>();
    // public virtual ICollection<VideoContent> VideoContents { get; set; } = new List<VideoContent>();
}
