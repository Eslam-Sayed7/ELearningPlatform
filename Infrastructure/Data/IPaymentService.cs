using Core.Entities;

namespace Infrastructure.Data;

public interface IPaymentService
{
    Task<Payment> ProcessPaymentAsync(Guid studentId, Guid courseId, Double amount, double discount);
    Task<Payment> GetPaymentByIdAsync(Guid paymentId);

    // Task<string> GenerateInvoiceAsync(Guid paymentId);
    // Task<bool> VerifyPaymentAsync(Guid paymentId);
    // Task<IEnumerable<Payment>> GetPaymentHistoryAsync(Guid studentId);
    // Task<Payment> RefundPaymentAsync(Guid paymentId);
}