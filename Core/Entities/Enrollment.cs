namespace Core.Entities;
public partial class Enrollment
{
    public Guid EnrollmentId { get; set; }
    public Guid StudentId { get; set; }
    public Guid CourseId { get; set; }
    public double? ProgressPercentage { get; set; } 
    public DateTime? EnrollmentDate { get; set; }
    public virtual Student Student { get; set; } = null!;
    public virtual Course Course { get; set; }
    public virtual ICollection<Progress> Progresses { get; set; }
    // public Guid PaymentId { get; set; }
    // public virtual Payment Payment { get; set; } = null!;
}
