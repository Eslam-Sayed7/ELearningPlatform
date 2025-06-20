﻿using Core.Entities;
using Infrastructure.Base;
using Infrastructure.Data;
using Infrastructure.Data.IServices;
using Infrastructure.Data.Models;
using Infrastructure.Dtos;
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
        private readonly AppDbContext _context;
        private readonly IRedisCachService _cache;
        public CourseController(ICourseService courseService,AppDbContext context, IRedisCachService cache)
        {
            _context = context;
            _cache = cache;
            _courseService = courseService;
        }

        [AllowAnonymous]
        [HttpGet("GetCourse")]
        public async Task<ActionResult<GetCourseDto>> GetCourseById(Guid courseid)  
        {
            var course = await _courseService.GetCourseByIdAsync(courseid);
            if (course == null)
            {
                return NotFound();
            }
            var instructor = course.Instructors?.FirstOrDefault();
            var firstName = instructor?.Appuser?.FirstName ?? "";
            var lastName = instructor?.Appuser?.LastName ?? "";
            var courseDto = new GetCourseDto()
           {
               CourseId = course.CourseId,
               CourseName = course.CourseName,
               Description = course.Description,
               Level = course.Level,
               Price = course.Price,
               Duration = course.Duration,
               ThumbnailUrl = course.ThumbnailUrl,
               Language = course.Language,
               Instructor = $"{firstName} {lastName}"
           }; 
            return Ok(courseDto);
        }

        
        [AllowAnonymous]
        [HttpGet("Popular")]
        public async Task<ActionResult<IEnumerable<CourseCardDto>>> GetCoursesPagse()
        {
            var courses = _cache.GetData<IEnumerable<CourseCardDto>>("PopularCourses");
            if(courses is not null)
            {
                return Ok(courses);
            } 
            courses = await _courseService.GetPopularCoursesPaged();
            _cache.SetData("PopularCourses" , courses);
            return Ok(courses);
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
            IList<CourseCardDto> result = new List<CourseCardDto>();

            foreach (var c in courses)
            {
                var crs = new CourseCardDto()
                {
                    CourseId = c.CourseId,
                    CourseName = c.CourseName,
                    CategoryName = c.Category.CategoryName,
                    ThumbnailUrl = c.ThumbnailUrl,
                    Price = c.Price
                };
                result.Add(crs);
            }
            // _logger.LogInformation("Courses by category retrieved successfully");
            return ((result.Any()) ? Ok(result) : Ok());
        }
    
        // Helper method to check if a course exists
        private async Task<bool> CourseExists(Guid id)
        {
            var course = await _courseService.GetCourseByIdAsync(id);
            if (course is not null)
                return true;
            return false;
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
        public async Task<ActionResult<Course>> AddCourse([FromBody] AddCourseModel request)
        {
            if (request == null)
            {
                return BadRequest("Invalid course data.");
            }

            var newCourse = new AddCourseModel()
            {
                CourseName = request.CourseName,
                Description = request.Description,
                Level = request.Level,
                Price = request.Price,
                Duration = request.Duration,
                ThumbnailUrl = request.ThumbnailUrl,
                Language = request.Language,
                // CreatedAt = DateTime.UtcNow,
                // UpdatedAt = DateTime.UtcNow
            };

            var createdCourse = await _courseService.AddCourse(newCourse);
            if (createdCourse == null)
            {
                return StatusCode(500, "An error occurred while creating the course.");
            }

            return Ok(createdCourse);
         }
        
         // [HttpPost("AddCourse")]
         // public async Task<ActionResult<Course>> AddCourse([FromBody] AddCourseModel model)
         // {
         //     if (model == null)
         //     {
         //         return BadRequest("Invalid course data.");
         //     }
         //
         //     try
         //     {
         //         // Call the service to add the course
         //         var createdCourse = await _courseService.AddCourse(model);
         //
         //         // Return the created course as a response
         //         return CreatedAtAction(nameof(GetCourseById), new { id = createdCourse.CourseId }, createdCourse);
         //     }
         //     catch (Exception ex)
         //     {
         //         // Handle the exception and return an error response
         //         return StatusCode(500, $"An error occurred while creating the course: {ex.Message}");
         //     }
         // }

        [HttpPost("delete")]
        public async Task<IActionResult> DeleteCourse(string id)
        {
            // Call the service method to delete the course
            var result = await _courseService.DeleteCourseAsync(id);
        
            if (!result)
            {
                return NotFound();  // If a course not found, return 404 Not Found
            }
        
            return Ok("Course deleted successfully");  // If successful, return 204 No Content
        }

    }
}