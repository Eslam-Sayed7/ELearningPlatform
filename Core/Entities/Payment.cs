using Core.Enums;

namespace Core.Entities;

public partial class Payment
{
    public Payment()
    {
        paymentStatus = PaymentStatus.Pending;
    }
    public Guid PaymentId { get; set; }

    public Guid StudentId { get; set; }

    public Guid CourseId { get; set; }

    public double Amount { get; set; }

    public PaymentStatus? paymentStatus { get; set; }
    public DateTime TransactionDate { get; set; } 

    public double? Discount { get; set; }

    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}
