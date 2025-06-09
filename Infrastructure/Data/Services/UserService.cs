using Core.Entities;
using Infrastructure.Base;
using Infrastructure.Data.IServices;
using Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Services;

public class UserService : IUserService
{
    private  readonly IUnitOfWork _unitOfWork;
    private IUserService _userServiceImplementation;

    public UserService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

   public async Task<AppUser> GetUserByIdAsync(Guid id)
  {
      var user = await _unitOfWork.Repository<AppUser>().GetByIdAsync(id);
      return user;
  }

  public Task<IEnumerable<AppUser>> GetAllStudentsAsync()
  {
      throw new NotImplementedException();
  }

  public Task<AppUser> UpdateUserAsync(UpdateUserModel model)
  {
      throw new NotImplementedException();
  }

  public async Task<AppUser> UpdateUserProfilePictureUrl(Guid Id, string Url)
  {
      var students = await _unitOfWork.Repository<Student>()
          .FindAsync(s => s.Id == Id, include: q => q.Include(s => s.Appuser));
      var student = students.FirstOrDefault();
      student.Appuser.ProfilePicture = Url;
      await _unitOfWork.Repository<Student>().UpdateEntity(student);
      await _unitOfWork.CompleteAsync();
      return student.Appuser;
  } 
    
}