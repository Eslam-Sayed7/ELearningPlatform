using Core.Entities;
using Infrastructure.Base;
using Core.Enums;
using Infrastructure.Data.Services;
using Infrastructure.Dtos;

namespace Infrastructure.Services.Enrollservice
{
    public class EnrollmentServices : IEnrollmentService 
    {  
        private readonly IUnitOfWork _unitOfWork;
        public EnrollmentServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork  = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        
       
        public async Task<EnrollmentDto> EnrollInCourse(Guid studentId , Guid courseId , double discount = 0)
        {
            var student =
            (await _unitOfWork.Repository<Student>()
                .FindAsync(s => s.Id == studentId)).FirstOrDefault();
            
            if (student == null)
                throw new KeyNotFoundException($"Student with {studentId} ID not found.");

            var course = (await _unitOfWork.Repository<Course>()
                    .FindAsync(c => c.CourseId == courseId)).FirstOrDefault();
            if (course == null)
                throw new KeyNotFoundException($"This course Is not found");

            var existingEnrollment = await CheckEnrollmentStatusAsync(studentId, courseId);
            
            if (existingEnrollment){

                var enroll = new EnrollmentDto {
                        Message = $"{studentId} is already enrolled in course {courseId}."
                    };
                
                return enroll;
            }

            _unitOfWork.BeginTransactionAsync();
            
            double amount = course.Price;
            
            PaymentService paymentService = new PaymentService(_unitOfWork);
            Payment payment = await paymentService.ProcessPaymentAsync(studentId, courseId , amount , discount); 

            if(payment.paymentStatus == PaymentStatus.Completed){
                var enrollment = new Enrollment
                {
                    StudentId = studentId,
                    CourseId = courseId,
                    EnrollmentDate = DateTime.UtcNow,
                    ProgressPercentage = 0,
                    PaymentId =  payment.PaymentId
                };
                await _unitOfWork.Repository<Enrollment>().AddAsync(enrollment);
                await _unitOfWork.CompleteAsync();

                var enroll = new EnrollmentDto {
                    Message = "You have Enrolled Successfully",
                    IsEnrolled = true
                };
                _unitOfWork.CommitTransactionAsync();
                _unitOfWork.CompleteAsync();
                return enroll;

            } else
            {
                _unitOfWork.RollbackTransactionAsync();
                var enroll = new EnrollmentDto {
                    Message = "Complete Course payment first to get enrolled",
                    IsEnrolled = false
                };
                return enroll;
            }
        }

        public async Task<bool> CheckEnrollmentStatusAsync(Guid studentId, Guid courseId)
        {
            return await _unitOfWork.Repository<Enrollment>().AnyAsync(e => e.StudentId == studentId && e.CourseId == courseId);
        }
    }
}