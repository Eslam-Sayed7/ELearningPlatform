using API.Extensions.Mappings;
using Core.Entities;
using Infrastructure.Base;
using Infrastructure.Data;
using Infrastructure.Data.Commands.CourseCommands;
using Infrastructure.Data.IServices;
using Infrastructure.Data.Models;
using Infrastructure.Data.Queries.CourseQueries;
using Infrastructure.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;
        private readonly IMediator _mediator;
        public CourseController(ICourseService courseService, IMediator mediator)
        {
            _mediator = mediator;
            _courseService = courseService;
        }

        [AllowAnonymous]
        [HttpGet("GetCourse")]
        public async Task<ActionResult<CourseCardDto>> GetCourseById(Guid courseid)  
        {
            var query = new GetCourseByIdQuery(courseid);
            var courseDto = await _mediator.Send(query);
            return (courseDto == null) ? NotFound() : Ok(courseDto);
        }

        
        [AllowAnonymous]
        [HttpGet("Popular")]
        public async Task<ActionResult<IEnumerable<CourseCardDto>>> GetCoursesPagse()
        {
            var query = new GetPopularCoursesQuery();
            var result  = await _mediator.Send(query);
            return (result is null ) ? NotFound(): Ok(result);
        }
       
        [Authorize]
        [HttpPost("CoursesByCategory")]
        public async Task<ActionResult<IList<CourseCardDto>>> GetCoursesByCategory(
            [FromBody] FilterByCategoryRequest request)
        {
            var spec = new Specification<Course>(c => c.Category.CategoryName == request.categoryName)
                .AddInclude(c => c.Include(x => x.Instructors))
                .AddInclude(c => c.Include(cat => cat.Category))
                .ApplyPaging(1 , 12)
                .ApplyOrderBy(q => q.OrderByDescending(c => c.CreatedAt));

            var courses = await _courseService.GetCoursesByCategory(spec);
            IList<CourseCardDto> result = courses.Select(c =>  c.ToDto()).ToList();
            return ((result.Any()) ? Ok(result) : Ok());
        }
    
        private async Task<bool> CourseExists(Guid id)
        {
            var course = await _courseService.GetCourseByIdAsync(id);
            return (course is null) ? false : true;
        }

        [Authorize(Roles = "Admin , Instructor , Student")]
        [HttpPost("Upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded");

            // Save the file to a specific location or cloud storage
            // Generate a URL for the uploaded file

            string fileUrl = "URL to the uploaded file"; // Example

            await Task.CompletedTask; // Dummy await
            return Ok(new { url = fileUrl });
        }
        
        [Authorize(Roles = "Admin , Instructor")]
        [HttpPost("AddCourse")]
        public async Task<ActionResult<CourseCardDto>> AddCourse([FromBody] AddCourseModel request)
        {
            if (request is null)
            {
                return BadRequest("Invalid course data.");
            }
            var command = new CreateCourseCommand(request);
            var createdCourse = await _mediator.Send(command);
            return createdCourse == null ? StatusCode(500, "An error occurred while creating the course.") : Ok(createdCourse);
        }
         
        [HttpPost("delete")]
        public async Task<IActionResult> DeleteCourse(string id)
        {
            var result = await _courseService.DeleteCourseAsync(id);
        
            if (!result)
            {
                return NotFound();  // If a course not found, return 404 Not Found
            }
        
            return Ok("Course deleted successfully");  // If successful, return 204 No Content
        }

    }
}