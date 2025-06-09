namespace Infrastructure.Data;
public class EnrollModel {
    public Guid? CourseId { get; set; }
    public Guid AppUserId { get; set; }
    public DateTime? EnrollmentDate { get; set; }
    public bool IsAuthenticated { get; set; }
    public Guid PaymentId { get; set; }

    // public string Username { get; set; }

    // public string? Progress { get; set; }

    // public string Token { get; set; }

    // public string Message { get; set; }
}

