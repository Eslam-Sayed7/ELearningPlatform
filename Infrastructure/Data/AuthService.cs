using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Core.Entities;
using Infrastructure.Data;
using Infrastructure.Data.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Infrastructure.Dtos;

namespace Infrastructure.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JWT _jwt;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IStudentService _studentService;
        
        public AuthService(UserManager<AppUser> userManager, IOptions<JWT> jwt ,
         RoleManager<IdentityRole> roleManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _jwt = jwt.Value;
            _roleManager = roleManager;
            _httpContextAccessor = httpContextAccessor;
        }
        
        public async Task<loggedUserDto> GetCurrentUser()
        {
            var user = _httpContextAccessor.HttpContext?.User;
        
            if (user != null && user.Identity.IsAuthenticated)
            {
                var username = user.FindFirst(ClaimTypes.Name)?.Value;
                var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                return new loggedUserDto
                {
                    Username = username,
                    UserId  = Guid.Parse(userId),
                    IsAuthenticated = true,
                };
            }

            return new loggedUserDto
            {
                Message = "Unauthorized",
                IsAuthenticated = false
            };
        }

        public async Task<AuthModel> RegisterUserAsync(RegisterModel model)
        {
            if(await _userManager.FindByEmailAsync(model.Email) is not  null)
                return new AuthModel { Message = "Email is already registered", IsAuthenticated = false };

            if(await _userManager.FindByNameAsync(model.Username) is not  null)
                return new AuthModel { Message = "UserName is already registered", IsAuthenticated = false };
            
            var user = new AppUser {
                UserName = model.Username,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName
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

            var student = new Student
            {
                Appuser = user
            };
            
            await _studentService.CreateStudentAsync(student);
            
            return new AuthModel
            {
                Email = user.Email,
                ExpiresOn = jwtSecurityToken.ValidTo,
                IsAuthenticated = true,
                Roles = new List<string> { "User" },
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
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

            var jwtSecurityToken = await CreateJwtToken(user);
            var rolesList = await _userManager.GetRolesAsync(user);
            
            authModel.IsAuthenticated = true;
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            authModel.Email = user.Email;
            authModel.Username = user.UserName;
            authModel.ExpiresOn = jwtSecurityToken.ValidTo;
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

            return result.Succeeded ? string.Empty : "Something went wrong";
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
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_jwt.DurationInDays),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }

    }

}