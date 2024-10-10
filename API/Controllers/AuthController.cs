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

        // do not need it for now [do not delete it please]
        
        // [Authorize]
        // [HttpGet]
        // public async Task<ActionResult<LoginResponseDto>> GetCurrentUserRole()
        // {
        //     return await _authService.GetCurrentUser();
        // }
        

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
            return Ok(result);
        }

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
                RefreshToken =  result.User.RefreshToken,
                RefreshTokenExpiryTime = result.User.RefreshTokenExpiryTime
            };
            
            return Ok(res);
        }

        [HttpPost("AddRole")]
        public async Task<IActionResult> AddRoleAsync([FromBody] AddRoleModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.AddRoleAsync(model);

            if (!string.IsNullOrEmpty(result))
                return BadRequest(result);

            return Ok(model);
        }
            
        
        // [Authorize]
        [HttpPost("GetRole")]
        public async Task<ActionResult<UserRoleDto>> GetRoleAsync([FromBody] GetRoleModel model)
        {
            var result = await _authService.GetRoleAsync(model);
            return Ok(result);
        }
        
     }
}
