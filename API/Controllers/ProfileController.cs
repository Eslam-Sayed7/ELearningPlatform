using Infrastructure.Data;
using Infrastructure.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Infrastructure.Data.IServices;
using Infrastructure.Data.Models;
using Infrastructure.Data.Services;
using Infrastructure.Dtos;
namespace TestApiJWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly IUserService _userService;
        
        public ProfileController(IUserService userService, IStudentService studentService)
        {
            _studentService = studentService;
            _userService = userService;
        }

        
        // [Authorize]
        [HttpPost("Details")]
        public async Task<IActionResult> GetProfileDetails([FromBody] ProfileDetailsModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
        
            var result = await _studentService.GetStudentByIdAsync(model.id);

            var profileresponse = new StudentProfileResponseDto()
            {
                FirstName = result.Appuser.FirstName,
                LastName = result.Appuser.LastName,
                ProfilePictureUrl = result.Appuser.ProfilePicture
            };
            return Ok(profileresponse);
        }
        
        // [Authorize]
        [HttpPost("Update")]
        public async Task<IActionResult> UpdateUser([FromBody]  UpdateUserModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
        
            var result = await _studentService.UpdateStudentAsync(model);
            
            if (result is null)
                return Unauthorized();
            var studentresponse  = new StudentProfileResponseDto()
            {
                FirstName = result.Appuser.FirstName,
                LastName = result.Appuser.LastName
            };
            return Ok(studentresponse);
        }

        [HttpPost("Upload")]
        public async Task<IActionResult> UploadProfilePicture([FromForm] IFormFile file, [FromForm] Guid Id)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Profile" ,"images", "profileImages");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolder, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            var fileUrl = $"images/profileImages/{fileName}";

            _userService.UpdateUserProfilePictureUrl(Id, fileUrl);
            
            return Ok(new { Url = fileUrl });
        }
    }
}