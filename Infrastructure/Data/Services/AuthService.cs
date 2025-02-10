using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using Core.Entities;
using Infrastructure.Data;
using Infrastructure.Data.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Infrastructure.Dtos;
using Infrastructure.Data.IServices;
using Infrastructure.Base;
using Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.WebEncoders.Testing;
using Serilog;
using Serilog.Core;

namespace Infrastructure.Services.Auth
{   
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JWT _jwt;
        private readonly Logger _logger;
        public AuthService(UserManager<AppUser> userManager, IOptions<JWT> jwt ,
         RoleManager<IdentityRole> roleManager  , IUnitOfWork unitOfWork) 
        {
            _userManager = userManager;
            _jwt = jwt.Value;
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
            _logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();
        }

        public async Task<AuthModel> RegisterUserAsync(RegisterModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            if (_userManager == null) throw new InvalidOperationException("UserManager is not initialized.");

            if (string.IsNullOrWhiteSpace(model.Username) || !Regex.IsMatch(model.Username, @"^[a-zA-Z0-9]+$"))
            {
                return new AuthModel
                {
                    Message = "Username can only contain letters and digits and cannot be empty.",
                    IsAuthenticated = false
                };
            }

            if(await _userManager.FindByEmailAsync(model.Email) is not  null)
                return new AuthModel { Message = "Email is already registered", IsAuthenticated = false };

            if(await _userManager.FindByNameAsync(model.Username) is not  null)
                return new AuthModel { Message = "UserName is already registered", IsAuthenticated = false };
            
            var user = new AppUser
            {
                Email = model.Email,
                UserName = model.Username,
                FirstName = model.FirstName,
                LastName = model.LastName,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            var UserCreation = await _userManager.CreateAsync(user, model.Password);
        
            var RoleSetting = await _userManager.AddToRoleAsync(user, "Student");
            
            if(!UserCreation.Succeeded || !RoleSetting.Succeeded){

                var errors = string.Empty;
                foreach(var error in ((!UserCreation.Succeeded)? UserCreation : RoleSetting).Errors){
                    errors += $"{error.Description},";
                }
                return new  AuthModel { Message = errors};
            }
            var jwtSecurityToken = await CreateJwtToken(user);

            
            user.RefreshTokenExpiryTime = jwtSecurityToken.ValidTo;
            user.RefreshToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            
            return new AuthModel
            {
                Message = $"User {user.Email} registered Successflly as User",
                User = user,
                Email = user.Email,
                IsAuthenticated = true,
                Roles = new List<string> { "Student" },
                Username = user.UserName
            };
        }

        public async Task<AuthModel> LoginAsync(TokenRequestModel model)
        {
            var authModel = new AuthModel();

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user is null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                authModel.Message = "Email or Password is incorrect!";
                return authModel;
            }

            var storedSecurityToken = user.RefreshToken;
            var RefreshTokenExpiryTime = user.RefreshTokenExpiryTime;

            var rolesList = await _userManager.GetRolesAsync(user);
            
            if (authModel.User == null) {
                authModel.User = new AppUser();
            }
            authModel.IsAuthenticated = true;
            authModel.User.RefreshToken = storedSecurityToken;
            authModel.Email = user.Email;
            authModel.Username = user.UserName;
            authModel.User.RefreshTokenExpiryTime = RefreshTokenExpiryTime;
            authModel.Roles = rolesList.ToList();

            return authModel;
        }

        public async Task<string> AddRoleAsync(AddRoleModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId.ToString());

            if (user is null || !await _roleManager.RoleExistsAsync(model.Role))
                return "Invalid user ID or Role";

            if (await _userManager.IsInRoleAsync(user, model.Role))
                return "User already assigned to this role";

            var result = await _userManager.AddToRoleAsync(user, model.Role);

            if (!result.Succeeded)
                return "Something went wrong while adding role";

            var jwtSecurityToken = await CreateJwtToken(user);
            user.RefreshToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            user.RefreshTokenExpiryTime = jwtSecurityToken.ValidTo;

            await _userManager.UpdateAsync(user);

            return "Role added successfully";
        } 

        private async Task<JwtSecurityToken> CreateJwtToken(AppUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            // _logger.Information("Key: {0}", Encoding.UTF8.GetBytes(_jwt.Key)); 
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_jwt.DurationInDays),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }
        private async Task<bool> AuthenticatedUser(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_jwt.Key);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = _jwt.Issuer,
                    ValidAudience = _jwt.Audience,
                    ValidateLifetime = true
                }, out SecurityToken validatedToken);
                
                return true;
            }
            catch
            {
                return false;
            }
        }
        
        // used when the token is expired and want to refresh it
        private async Task<ClaimsPrincipal> GetPrincipalFromExpiredToken(string token)
        {
            var SecKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            if (Encoding.UTF8.GetByteCount(_jwt.Key) >= 16)
            {
                SecKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            }
            else
            {
                var keyBytes = Encoding.UTF8.GetBytes(_jwt.Key);
                Array.Resize(ref keyBytes, 16);
                SecKey = new SymmetricSecurityKey(keyBytes);    
            }
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = SecKey,
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);

            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }
        public async Task<UserRoleDto> GetRoleAsync(GetRoleModel model)
        {
            
            var user = await _userManager.FindByIdAsync(model.UserId);
            var roles = await _userManager.GetRolesAsync(user);

            return new UserRoleDto()
            {
                Roles = roles
            };
        }
        
    }

}