using Core.Entities;
using Infrastructure.Base;
using Infrastructure.Data.Models;
using Infrastructure.Data.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Data.IServices;

public class StudentService : IStudentService
{
    private readonly IUnitOfWork _unitOfWork;

    public StudentService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Student> UpdateStudentAsync(UpdateUserModel model)
    {

        var students = await _unitOfWork.Repository<Student>()
            .FindAsync(s => s.Id == model.Id , include: q => q.Include(s => s.Appuser));
        var student = students.FirstOrDefault();
        student.Appuser.FirstName = model.FirstName;
        student.Appuser.LastName = model.LastName;
        await _unitOfWork.Repository<Student>().UpdateEntity(student);
        await _unitOfWork.CompleteAsync();
        return student;
    }

    public async Task<Student> CreateStudentAsync(Student student)
    {
        if (student == null)
            throw new ArgumentException(nameof(student));

        await _unitOfWork.Repository<Student>().AddAsync(student);
        await _unitOfWork.CompleteAsync();
        return student;
    }

    public async Task<Student> GetStudentByIdAsync(Guid id)
    {
        // eager loading
        var students = await _unitOfWork.Repository<Student>()
            .FindAsync(s => s.Id == id , include: q => q.Include(s => s.Appuser));
        var student = students.FirstOrDefault();

        return student;
    }

    public async Task<IEnumerable<Student>> GetAllStudentsAsync()
    {
        var students = await _unitOfWork.Repository<Student>()
            .FindAsync(s => true , include: q => q.Include(s => s.Appuser));
                                            // eager loading
        return students;
    }

    
}