namespace Core.Entities;

public class Instructor
{
    public Guid Id { get; set; }
    public string UserId { get; set; }
    public AppUser Appuser { get; set; }
    public string Expertise { get; set; }
    public string Bio { get; set; }
    public List<Course> CoursesTeach { get; set; } = new List<Course>();
    public virtual ICollection<InstructorsToCourse> InstructorsToCourses { get; set; } = new List<InstructorsToCourse>();
}