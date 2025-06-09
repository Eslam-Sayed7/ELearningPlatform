using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogger<SearchController> _logger;
        public SearchController(AppDbContext context , ILogger<SearchController> logger)
        {
            _logger = logger; 
            _context = context;
        }

        [AllowAnonymous]
        [HttpGet("Search")]
        public async Task<ActionResult<IEnumerable<Course>>> SearchCourses([FromQuery] string query)
        {
            _logger.LogInformation("Searching for courses with query: {query}", query);
            if (string.IsNullOrWhiteSpace(query))
            {
                return BadRequest("Search query cannot be empty.");
            }
            
            var courses = await _context.Courses
                .Where(c => c.CourseName.ToLower().Contains(query.ToLower()) || c.Description.ToLower().Contains(query.ToLower()))
                .ToListAsync();
            
            _logger.LogInformation("Found {count} courses", courses.Count);
        
            return Ok(courses);
        }
        
        [AllowAnonymous]
        [HttpGet("SearchWithFiltering")]
        public async Task<ActionResult<IEnumerable<Course>>> SearchCoursesWithFiltering(
        [FromQuery] string query,
        [FromQuery] string? level,
        [FromQuery] double? minPrice,
        [FromQuery] double? maxPrice,
        [FromQuery] string language)
        {

            var coursesQuery = _context.Courses.AsQueryable();

            bool isQueryValid = !string.IsNullOrWhiteSpace(query);
            bool isLevelValid = !string.IsNullOrWhiteSpace(level);
            bool isMinPriceValid = minPrice.HasValue;
            bool isMaxPriceValid = maxPrice.HasValue;
            bool isLanguageValid = !string.IsNullOrWhiteSpace(language);


            if (isQueryValid)
            {
                coursesQuery = coursesQuery.Where(c => c.CourseName.ToLower().Contains(query.ToLower()) || c.Description.ToLower().Contains(query.ToLower()));
            }

            if (isLevelValid)
            {
                coursesQuery = coursesQuery.Where(c => c.Level.ToLower() == level.ToLower());
            }

            if (isMinPriceValid)
            {
                coursesQuery = coursesQuery.Where(c => c.Price >= minPrice.Value);
            }
            if (isMaxPriceValid)
            {
                coursesQuery = coursesQuery.Where(c => c.Price <= maxPrice.Value);
            }

            if (isLanguageValid)
            {
                coursesQuery = coursesQuery.Where(c => c.Language.ToLower() == language.ToLower());
            }

            var courses = await coursesQuery.ToListAsync();

            return Ok(courses);
        }
    }
}
