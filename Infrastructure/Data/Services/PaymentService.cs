// using Core.Enums;
// using Infrastructure.Base;
// using Infrastructure.Data.IServices;
// using Core.Entities;
//
// namespace Infrastructure.Data.Services;
//
// public class PaymentService : IPaymentService
// {
//     private readonly IUnitOfWork _unitOfWork;
//     public PaymentService(IUnitOfWork unitOfWork) {
//
//         _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
//     }
//
//     public Task<Payment> GetPaymentByIdAsync(Guid paymentId)
//     {
//         throw new NotImplementedException();
//     }
//
//     public async Task<Payment> ProcessPaymentAsync(Guid studentId, Guid courseId, double amount, double discount = 0)
//     {
//         var payment = new Payment
//         {
//             PaymentId = Guid.NewGuid(),
//             StudentId = studentId,
//             CourseId = courseId,
//             Amount = amount,
//             Discount = discount,
//             TransactionDate = DateTime.UtcNow,
//             paymentStatus = PaymentStatus.Pending
//         };
//
//         await _unitOfWork.Repository<Payment>().AddAsync(payment);
//         payment.paymentStatus = PaymentStatus.Completed;
//         await _unitOfWork.CompleteAsync();
//
//         return payment;
//     }
//
//     // public Task<bool> VerifyPaymentAsync(Guid paymentId)
//     // {
//     //     throw new NotImplementedException();
//     // }
// }