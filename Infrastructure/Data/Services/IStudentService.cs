using Core.Entities;

namespace Infrastructure.Data.Services;

public interface IStudentService
{
    Task<Student> GetStudentByIdAsync(Guid id);
    Task<IEnumerable<Student>> GetAllStudentsAsync();
    Task<Student> CreateStudentAsync(Student student);
}