namespace Core.Entities;

public partial class InstructorsToCourse
{
    public Guid InstructorToCourseId { get; set; } = Guid.NewGuid();

    public Guid InstructorId { get; set; }

    public Guid CourseId { get; set; }


}
