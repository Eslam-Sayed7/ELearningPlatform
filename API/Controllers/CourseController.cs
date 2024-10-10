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
