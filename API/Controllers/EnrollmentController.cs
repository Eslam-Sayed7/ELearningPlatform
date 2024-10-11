using Core.Entities;
using Infrastructure.Data;
using Infrastructure.Data.Models;
using Infrastructure.Dtos;
using Infrastructure.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Services.Enrollservice;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnrollmentController : Controller
    {   
        private readonly IEnrollmentService _enrollService;
        
        [Authorize]
        [HttpPost("Enroll")]
        public async Task<ActionResult<EnrollmentDto>> EnrollInCourse([FromBody] EnrollmentRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var enrollmentResult = await _enrollService.EnrollInCourse(request.CourseId, request.StudentId);
            EnrollmentDto enroll = new EnrollmentDto
            {
                Message = enrollmentResult.Message
            };
            return Ok(enroll);
        }

        [HttpPost("CheckEnroll")]
        public async Task<ActionResult<Enrollment>> GetEnrollment([FromBody] CheckEnrollmentModel request)
        {
            var enrollment = await _enrollService.CheckEnrollmentStatusAsync(request.UserId , request.CourseId);
            if (enrollment == false)
            {
                return NotFound();
            }
            return Ok(enrollment);
        }
        
        
        
        
    }
}
