namespace Infrastructure.Dtos;
public class EnrollmentRequestDto
{
    public Guid CourseId { get; set; }        
    public Guid StudentId { get; set; }          
    public DateTime EnrollmentDate { get; set; } = DateTime.UtcNow; // The date of enrollment
    // public string PaymentId { get; set; }       
}
