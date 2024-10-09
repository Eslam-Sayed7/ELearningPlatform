using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CourseAPIFinale.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CoursesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CoursesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetCourseById(Guid id)
        {
            var course = await _context.Courses.FindAsync(id);

            if (course == null)
            {
                return NotFound();
            }

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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetAllCourses()
        {
            var courses = await _context.Courses.ToListAsync();

            return Ok(courses);
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

}
