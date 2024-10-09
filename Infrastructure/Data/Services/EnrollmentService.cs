using Infrastructure.Data;
using Core.Entities;
using Infrastructure.Base;
using Core.Enums;
using Infrastructure.Dtos;

namespace Infrastructure.Data
{
    public class EnrollmentServices : IEnrollmentService 
    {  
        private readonly IUnitOfWork _unitOfWork;
        public EnrollmentServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork  = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }
        
       
        public async Task<EnrollmentDto> EnrollInCourse(Guid studentId , Guid courseId )
        {
        
            var student =
            (await _unitOfWork.Repository<Student>()
                .FindAsync(s => s.Id == studentId)).FirstOrDefault();
            
                if (student == null)
            throw new KeyNotFoundException($"Student with {studentId} ID not found.");

            var course =
                (await _unitOfWork.Repository<Course>()
                    .FindAsync(c => c.CourseId == courseId)).FirstOrDefault();
            if (course == null)
                throw new KeyNotFoundException($"Course with ID {courseId} not found.");

            var existingEnrollment = await CheckEnrollmentStatusAsync(studentId, courseId);
            if (existingEnrollment){

                var enroll = new EnrollmentDto {
                        Message = $"{studentId} is already enrolled in course {courseId}."
                    };
                
                return enroll;
            }

            double amount = 10.1; //TODO
            double discount = 0;

            PaymentService paymentService = new PaymentService(_unitOfWork);
            Payment payment = await paymentService.ProcessPaymentAsync(studentId, courseId , amount , discount); 

            if(payment.paymentStatus == PaymentStatus.Completed){
                var enrollment = new Enrollment
                {
                    StudentId = studentId,
                    CourseId = courseId,
                    EnrollmentDate = DateTime.UtcNow,
                    Progress = 0,
                    PaymentId =  payment.PaymentId
                };
                await _unitOfWork.Repository<Enrollment>().AddAsync(enrollment);
                await _unitOfWork.CompleteAsync();

                var enroll = new EnrollmentDto {
                    Message = "You have Enrolled Successfully"
                };
            
                return enroll;

            } else {
                var enroll = new EnrollmentDto {
                    Message = "Check Course payment first to get enrolled"
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