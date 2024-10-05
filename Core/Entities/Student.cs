namespace Core.Entities;

public class Student
{
    public Guid Id { get; set; }
    public string UserId { get; set; }
    public AppUser Appuser { get; set; }
    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    
}