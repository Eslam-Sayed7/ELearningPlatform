using Core.Entities;
using Infrastructure.Data.Models;

namespace Infrastructure.Data.Services;

public interface IStudentService
{
    Task<Student> GetStudentByIdAsync(Guid id);
    Task<IEnumerable<Student>> GetAllStudentsAsync();
    Task<Student> CreateStudentAsync(Student student);
    Task<Student> UpdateStudentAsync(UpdateUserModel model);
}