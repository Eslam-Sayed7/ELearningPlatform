using Core.Entities;
using Infrastructure.Data;
using Infrastructure.Dtos;

namespace Infrastructure.Services.Enrollservice;
    public interface IEnrollmentService {
        Task<EnrollmentDto> EnrollInCourse(Guid studentId , Guid courseId );

        Task<bool> CheckEnrollmentStatusAsync(Guid studentId , Guid courseId );
    }

