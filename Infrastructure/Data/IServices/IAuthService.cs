using Core.Entities;
using Infrastructure.Data;
using Infrastructure.Dtos;


namespace Infrastructure.Services.Auth;
    public interface IAuthService {
        Task<AuthModel> RegisterUserAsync(RegisterModel model);

        Task<AuthModel> LoginAsync(TokenRequestModel model);

        Task<string> AddRoleAsync(AddRoleModel model);

        
    }
