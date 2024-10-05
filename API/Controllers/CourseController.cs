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
        public async Task<ActionResult<Course>> GetCourseById(int id)
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

    }

}
