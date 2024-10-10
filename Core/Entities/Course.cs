namespace Core.Entities;

public partial class Course
{
    public Guid CourseId { get; set; } = Guid.NewGuid();
    public string CourseName { get; set; } = null!;
    public string? Description { get; set; }
    public Guid? CategoryId { get; set; }
    public string Level { get; set; }
    public decimal Price { get; set; }
    public int Duration { get; set; }
    public string ThumbnailUrl { get; set; }
    public string Language { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int LastSectionSequence { get; set; }   // used to sort descending sections
    public virtual Category Category { get; set; }
    public virtual ICollection<CourseSection> CourseSections { get; set; } = new List<CourseSection>();
    public virtual ICollection<CourseMaterial> CourseMaterials { get; set; } = new List<CourseMaterial>();
    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    public virtual ICollection<Instructor> Instructors { get; set; } = new List<Instructor>();
    public virtual ICollection<InstructorsToCourse> InstructorsToCourses { get; set; } = new List<InstructorsToCourse>();
}
