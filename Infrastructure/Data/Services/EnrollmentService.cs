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
            {
                return new EnrollmentDto()
                {
                    Message = $"Student with {studentId} ID not found.",
                    IsEnrolled = false
                };
            }

            var course = (await _unitOfWork.Repository<Course>()
                    .FindAsync(c => c.CourseId == courseId)).FirstOrDefault();
            if (course == null)
                return new EnrollmentDto()
                {
                    Message = $"This course Is not found",
                    IsEnrolled = false
                };

            var existingEnrollment = await CheckEnrollmentStatusAsync(studentId, courseId);
            
            if (existingEnrollment){

                return new EnrollmentDto {
                    Message = $"{studentId} is already enrolled in course {courseId}."
                };
            }

            _unitOfWork.BeginTransactionAsync();
            
            double amount = course.Price;
            
            // TODO NOT COMPLETE FEATURE
            // PaymentService paymentService = new PaymentService(_unitOfWork);
            // Payment payment = await paymentService.ProcessPaymentAsync(studentId, courseId , amount , discount); 
            //
            // if(payment.paymentStatus == PaymentStatus.Completed){
            //
            // } else
            // {
            //     _unitOfWork.RollbackTransactionAsync();
            //     var enroll = new EnrollmentDto {
            //         Message = "Complete Course payment first to get enrolled",
            //         IsEnrolled = false
            //     };
            //     return enroll;
            // }

            var enrollment = new Enrollment
            {
                StudentId = studentId,
                CourseId = courseId,
                EnrollmentDate = DateTime.UtcNow,
                ProgressPercentage = 0,
                // PaymentId =  payment.PaymentId
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

        }

        public async Task<bool> CheckEnrollmentStatusAsync(Guid studentId, Guid courseId)
        {
            return await _unitOfWork.Repository<Enrollment>().AnyAsync(e => e.StudentId == studentId && e.CourseId == courseId);
        }
    }
}