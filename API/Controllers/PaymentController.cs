// using Infrastructure.Dtos;
// using Infrastructure.Data.IServices;
// using Microsoft.AspNetCore.Mvc;
//
// namespace API.Controllers
// {
//     public class PaymentController : Controller
//     {
//         private readonly IPaymentService _service;
//
//         public PaymentController(IPaymentService service)
//         {
//             _service = service;
//         }
//
//         [HttpPost("Payment")]
//         public async Task<IActionResult> ProcessPayment([FromBody] PaymentRequestDto request)
//         {
//             if (!ModelState.IsValid)
//             {
//                 return BadRequest(ModelState);
//             }
//
//             var payment = await _service.ProcessPaymentAsync(request.StudentId, request.CourseId, request.Amount, request.Discount);
//
//             var paymentDto = new PaymentDto
//             {
//                 PaymentId = payment.PaymentId,
//                 StudentId = payment.StudentId,
//                 CourseId = payment.CourseId,
//                 Amount = payment.Amount,
//                 Discount = (double)payment.Discount,
//                 PaymentStatus = payment.paymentStatus.ToString(),
//                 TransactionDate = payment.TransactionDate
//             };
//
//             return Ok(paymentDto);
//         }
//     }
// }
