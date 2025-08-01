using Infrastructure.Data;
using Infrastructure.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Infrastructure.Dtos;
using Infrastructure.Data.Services;
using Infrastructure.Data.IServices;
using Core.Entities;
using Infrastructure.Data.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IStudentService _studentService;
        

        public AuthController(IAuthService authService , IStudentService studentService)
        {
            _authService = authService;
            _studentService = studentService;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.RegisterUserAsync(model);
            
            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            var student = new Student {
                Appuser = result.User                
            };

            var addingStudent = await _studentService.CreateStudentAsync(student);
            if (addingStudent == null)
                return BadRequest("Failed to create student profile.");
            
            SetRefreshTokenInCookie(result.RefreshToken, result.RefreshTokenExpiryTime.Value);
            var res = new LoginResponseDto {
                Message = result.Message,
                IsAuthenticated = result.IsAuthenticated,
                Username = result.Username,
                Email = result.Email,
                Roles = result.Roles,
                Token =  result.User.Token,
                RefreshTokenExpiryTime = result.User.RefreshTokenExpiryTime
            };
            return Ok(res);
        }
        
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> GetTokenAsync([FromBody] TokenRequestModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.LoginAsync(model);

            if (!result.IsAuthenticated)
                return Unauthorized(result.Message);

            var res =  new LoginResponseDto {
                Message = result.Message,
                IsAuthenticated = result.IsAuthenticated,
                Username = result.Username,
                Email = result.Email,
                Roles = result.Roles,
                Token =  result.User.Token,
                RefreshToken = result.RefreshToken,
                RefreshTokenExpiryTime = result.User.RefreshTokenExpiryTime
            };
            
            if(!string.IsNullOrEmpty(result.RefreshToken))
            {
                SetRefreshTokenInCookie(res.RefreshToken, res.RefreshTokenExpiryTime.Value);
            }
            if(!string.IsNullOrEmpty(result.RefreshToken))
            {
                SetRefreshTokenInCookie(res.RefreshToken, res.RefreshTokenExpiryTime.Value);
            }
            return Ok(res);
        }
        
        [Authorize(Roles = "Admin")]
        
        [Authorize(Roles = "Admin")]
        [HttpPost("AddRole")]
        public async Task<IActionResult> AddRoleAsync([FromBody] AddRoleModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.AddRoleAsync(model);

            if (!result.IsSuccess)
            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(model);
        }
            
        [Authorize(Roles = "Admin , Instructor , Student")]
        [HttpPost("GetRole")]
        public async Task<ActionResult<UserRoleDto>> GetRoleAsync([FromBody] GetRoleModel model)
        {
            var result = await _authService.GetRoleAsync(model);
            if (result.Roles.Count == 0)
            {
                return NotFound();
            }
            return Ok(result);
        }
        
        [HttpGet("refreshToken")]
        public async Task<IActionResult> RefreshToken()
        {
            var refreshToken = HttpContext.Request.Cookies["refreshToken"];
            var result = await _authService.RefreshTokenAsync(refreshToken);

            if (!result.IsAuthenticated)
                return BadRequest(result);

            SetRefreshTokenInCookie(result.RefreshToken, result.RefreshTokenExpiryTime.Value);
            return Ok(result);
        }
        
        [HttpPost("revokeToken")]
        public async Task<IActionResult> RevokeToken([FromBody] RevokeTokenModel model)
        {
            var token =  model.Token ?? HttpContext.Request.Cookies["refreshToken"];
            if (string.IsNullOrEmpty(token))
                return BadRequest("token is required!");
            
            var result = await _authService.RevokeTokenAsync(token);
            if (!result)
                return BadRequest("toke is invalid!");

            HttpContext.Response.Cookies.Delete("refreshToken");
            return Ok();
        }
        private void SetRefreshTokenInCookie(string refreshToken, DateTime expires)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = expires.ToLocalTime(),
                SameSite = SameSiteMode.Lax,
                Secure = false ,
                Domain = "elearn" // Set your domain here
            };
            HttpContext.Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        }
     }
}
