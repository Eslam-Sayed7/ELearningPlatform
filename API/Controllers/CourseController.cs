using Core.Entities;
using Infrastructure.Base;
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
        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        // [HttpGet("{id}")]
        [HttpPost("GetCourse")]
        public async Task<ActionResult<Course>> GetCourseById(GetCourseModel model)
        {
            var course = await _courseService.GetCourseByIdAsync(model.CourseId);
            // if (course == null)
            // {
            //     return NotFound();
            // }
            var courseDto = new Course
            {
                
                CourseName = course.CourseName,
                Description = course.Description,
                Level = course.Level,
                Price = course.Price,
                Duration = course.Duration,
                ThumbnailUrl = course.ThumbnailUrl,
                Language = course.Language,
                UpdatedAt = course.UpdatedAt
            };
                return Ok(courseDto);
        }

        [HttpGet("Popular")]
        public async Task<ActionResult<IEnumerable<CourseCardDto>>> GetCoursesPagse()
        {
            var courses = await _courseService.GetPopularCoursesPaged();
            
            return Ok(courses);
        }

        [HttpPost("CoursesByCategory")]
        public async Task<ActionResult<IList<CourseCardDto>>> GetCoursesByCategory([FromBody] FilterByCategoryRequest request)
        {
            var spec = new Specification<Course>(c => c.Category.CategoryName == request.categoryName)
                .AddInclude(c => c.Include(x => x.Instructors))
                .AddInclude(c => c.Include(cat => cat.Category))
                .ApplyOrderBy(q => q.OrderByDescending(c => c.CreatedAt));
            
            var courses = await _courseService.GetCoursesByCategory(spec);
            IList <CourseCardDto> result = new List<CourseCardDto>();

            foreach (var c in courses)
            {
                var crs = new CourseCardDto()
                {
                    CourseId = c.CourseId,
                    CourseName = c.CourseName,
                    Category = c.Category.CategoryName,
                    ThumbnailUrl = c.ThumbnailUrl,
                    Price = c.Price
                };
                result.Add(crs);
            }
            return ((result.Any()) ? Ok(result) : Ok()); 
        }
        // Delete a course
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(Guid id)
        {
            var course = await _context.Courses.FindAsync(id);

            if (course == null)
            {
                return NotFound();
            }

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Helper method to check if a course exists
        private bool CourseExists(Guid id)
        {
            return _context.Courses.Any(c => c.CourseId == id);
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

        //[HttpPost]
        //public async Task<ActionResult<Course>> AddCourse([FromBody] Course courseDto)
        //{
        //    if (courseDto == null)
        //    {
        //        return BadRequest("Course data is required.");
        //    }

        //    var course = new Course
        //    {
        //        CourseName = courseDto.CourseName,
        //        Description = courseDto.Description,
        //        Level = courseDto.Level,
        //        Price = courseDto.Price,
        //        Duration = courseDto.Duration,
        //        ThumbnailUrl = courseDto.ThumbnailUrl,
        //        Language = courseDto.Language,
        //        Tags = courseDto.Tags,
        //        Category = courseDto.Category,
        //        CreatedAt = DateTime.UtcNow,
        //        UpdatedAt = DateTime.UtcNow
        //    };

        //    _context.Courses.Add(course);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction(nameof(GetCourseById), new { id = course.Id }, course); // Return the new course with its ID
        //}


    }

        
        //
        // [HttpPost("AddCourse")]
        // public async Task<ActionResult<Course>> AddCourse([FromBody] AddCourseModel request)
        // {
        //     // if (!ModelState.IsValid)
        //         // return BadRequest(ModelState);
        //     var course = await _courseService.AddCourse(request);
        //     return Ok(course);
        // }
        
    }
}
