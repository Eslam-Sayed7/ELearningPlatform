using Core.Entities;
using Infrastructure.Base;
using Infrastructure.Data;
using Infrastructure.Data.IServices;
using Infrastructure.Data.Models;
using Infrastructure.Dtos;
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
        public CourseController(ICourseService courseService,AppDbContext context)
        {
            _context = context;
            _courseService = courseService;
        }

        // [HttpGet("Get")]
        // // [HttpPost("GetCourse")]
        // public async Task<ActionResult<GetCourseDto>> GetCourseById([FromBody] GetCourseModel model)
        // {
        //     // var course = await _courseService.GetCourseByIdAsync(model.CourseId);
        //     var course = await _courseService.GetCourseByIdAsync(model.CourseId);
        //     if (course == null)
        //     {
        //         return NotFound();
        //     }
        //     var courseDto = new GetCourseDto()
        //     {
        //         CourseName = course.CourseName,
        //         Description = course.Description,
        //         Level = course.Level,
        //         Price = course.Price,
        //         Duration = course.Duration,
        //         ThumbnailUrl = course.ThumbnailUrl,
        //         Language = course.Language,
        //         Instructor = course.Instructor
        //     };
        //     return Ok(courseDto);
        // }

        
        [HttpGet("Popular")]
        public async Task<ActionResult<IEnumerable<CourseCardDto>>> GetCoursesPagse()
        {
            var courses = await _courseService.GetPopularCoursesPaged();

            return Ok(courses);
        }
        
        [HttpPost("CoursesByCategory")]
        public async Task<ActionResult<IList<CourseCardDto>>> GetCoursesByCategory(
            [FromBody] FilterByCategoryRequest request)
        {
            var spec = new Specification<Course>(c => c.Category.CategoryName == request.categoryName)
                .AddInclude(c => c.Include(x => x.Instructors))
                .AddInclude(c => c.Include(cat => cat.Category))
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

            return ((result.Any()) ? Ok(result) : Ok());
        }

        // Delete a course
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteCourse(Guid id)
        //{
        //    var course = await _courseService.GetCourseByIdAsync(id);

        //    if (course == null)
        //    {
        //        return NotFound();
        //    }

        //    // TODO delete funct want to be implemented first inside te unitofworkclass
        //    // _context.Courses.Remove(course);
        //    // await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        // Helper method to check if a course exists
        private async Task<bool> CourseExists(Guid id)
        {
            var course = await _courseService.GetCourseByIdAsync(id);
            if (course is not null)
                return true;
            return false;
        }


        [HttpPost("Upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded");

            // Save the file to a specific location or cloud storage
            // Generate a URL for the uploaded file

            string fileUrl = "URL to the uploaded file"; // Example

            return Ok(new { url = fileUrl });
        }

        //[HttpPost("AddCourse")]
        //public async Task<ActionResult<Course>> AddCourse([FromBody] AddCourseModel request)
        //{
        //    if (request == null)
        //    {
        //        return BadRequest("Invalid course data.");
        //    }

        //    var newCourse = new Course
        //    {
        //        CourseName = request.CourseName,
        //        Description = request.Description,
        //        Level = request.Level,
        //        Price = request.Price,
        //        Duration = request.Duration,
        //        ThumbnailUrl = request.ThumbnailUrl,
        //        Language = request.Language,
        //        CreatedAt = DateTime.UtcNow,
        //        UpdatedAt = DateTime.UtcNow
        //    };

        //    var createdCourse = await _courseService.AddCourseAsync(newCourse);
        //    if (createdCourse == null)
        //    {
        //        return StatusCode(500, "An error occurred while creating the course.");
        //    }

        //    return Ok(createdCourse);
        //}
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

        //public async Task<IActionResult> DeleteCourse(string id)
        //{
        //    // Call the service method to delete the course
        //    var result = await _courseService.DeleteCourseAsync(id);

        //    //if (!result)
        //    //{
        //    //    return NotFound();  // If course not found, return 404 Not Found
        //    //}

        //    return NoContent();  // If successful, return 204 No Content
        //}

        // [HttpPost("delete")]
        // public async Task<IActionResult> DeleteCourse(GetCourseModel model)
        // {
        //     var course = await _courseService.GetCourseByIdAsync(model.CourseId);
        //
        //     if (course == null)
        //     {
        //         return NotFound();
        //     }
        //
        //     _context.Courses.Remove(course);
        //
        //     await _context.SaveChangesAsync();
        //
        //     return NoContent();
        // }

    }
}