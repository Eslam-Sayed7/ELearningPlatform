using System.Diagnostics;
using Core.Entities;
using Core.Enums;
using Infrastructure.Base;

namespace Infrastructure.Services.Pay;

public class PaymentService : IPaymentService
{
    private readonly IUnitOfWork _unitOfWork;
    public PaymentService(IUnitOfWork unitOfWork) {

        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    // public asnyc Task<string> GenerateInvoiceAsync(Guid paymentId)
    // {
        
    // }

    public Task<Payment> GetPaymentByIdAsync(Guid paymentId)
    {
        throw new NotImplementedException();
    }

    public async Task<Payment> ProcessPaymentAsync(Guid studentId, Guid courseId, double amount
        , double discount = 0)
    {
        var payment = new Payment {
            StudentId = studentId,
            CourseId = courseId,
            Amount = amount,
            // PaymentMethod = paymentMethod,
            Discount = discount,
        };

        await _unitOfWork.Repository<Payment>().AddAsync(payment);
        
        payment.paymentStatus = PaymentStatus.Completed;
        await _unitOfWork.CompleteAsync();
        
        return payment;
    }

    // public Task<bool> VerifyPaymentAsync(Guid paymentId)
    // {
    //     throw new NotImplementedException();
    // }
}