using Core.Entities;
using Infrastructure.Data.Models;

    namespace Infrastructure.Data.IServices;

public interface IUserService
{
    Task<AppUser> GetUserByIdAsync(Guid id);
    // Task<bool> ValidTokenAsync(string token);
    Task<IEnumerable<AppUser>> GetAllStudentsAsync();
    Task<AppUser> UpdateUserAsync(UpdateUserModel model);

    Task<AppUser> UpdateUserProfilePictureUrl(Guid Id, string Url);
}