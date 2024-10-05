// using Infrastructure.Data;
// using Infrastructure.Services.Auth;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;
// using System.Threading.Tasks;
// using Infrastructure.Dtos;
// namespace TestApiJWT.Controllers
// {
//     [Route("api/[controller]")]
//     [ApiController]
//     public class StudentController : ControllerBase
//     {
//         private readonly IAuthService _authService;

//         public StudentController(IAuthService authService)
//         {
//             _authService = authService;
//         }

//         [HttpPost("register")]
//         public async Task<IActionResult> RegisterAsync([FromBody] RegisterModel model)
//         {
//             if (!ModelState.IsValid)
//                 return BadRequest(ModelState);

//             var result = await _authService.RegisterUserAsync(model);
            
//             if (!result.IsAuthenticated)
//                 return BadRequest(result.Message);

//             return Ok(result);
//         }

//         [HttpPost("login")]
//         public async Task<IActionResult> GetTokenAsync([FromBody] TokenRequestModel model)
//         {
//             if (!ModelState.IsValid)
//                 return BadRequest(ModelState);

//             var result = await _authService.LoginAsync(model);

//             if (!result.IsAuthenticated)
//                 return Unauthorized(result.Message);

//             return Ok(result);
//         }

//         [HttpPost("addrole")]
//         public async Task<IActionResult> AddRoleAsync([FromBody] AddRoleModel model)
//         {
//             if (!ModelState.IsValid)
//                 return BadRequest(ModelState);

//             var result = await _authService.AddRoleAsync(model);

//             if (!string.IsNullOrEmpty(result))
//                 return BadRequest(result);

//             return Ok(model);
//         }
//      }
// }s