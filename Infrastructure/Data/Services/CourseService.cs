using API.Extensions.Mappings;
using Core.Entities;
using Infrastructure.Base;
using Infrastructure.Data.IServices;
using Infrastructure.Data.Models;
using Infrastructure.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Services;

public class CourseService : ICourseService
{
    public readonly IUnitOfWork _UnitOfWork;

    public CourseService(IUnitOfWork unitOfWork)
    {
        _UnitOfWork = unitOfWork;
    }
    //TODO
    public async Task<CourseCardDto> GetCourseCardByIdAsync(Guid Id)
    {
        
        var searchcourse = await _UnitOfWork.Repository<Course>()
            .FindAsync(c => c.CourseId.ToString().ToUpper() == Id.ToString().ToUpper() , 
                include: q => q.Include( I => I.InstructorsToCourses)
                    .Include(q => q.Category));
        
        var course = searchcourse.FirstOrDefault();
        return new CourseCardDto()
        {
            CourseId = course.CourseId,
            CourseName = course.CourseName,
            Instructor = course.Instructors.FirstOrDefault().ToString(),
            CategoryName = course.Category.ToString(),
            ThumbnailUrl = course.ThumbnailUrl,
            Price = course.Price,
            CourseDescription = course.Description
        };

    }

    public async Task<Course> GetCourseByIdAsync(Guid Id)
    {
        var searchcourse = await _UnitOfWork.Repository<Course>()
            .FindAsync(c => c.CourseId.ToString().ToUpper() == Id.ToString().ToUpper() , 
                include: q => q.Include( I => I.InstructorsToCourses)
                    .Include(c => c.Category));
        
        var course = searchcourse.FirstOrDefault();
        return course;
    }

    public async Task<IList<CourseCardDto>> GetPopularCoursesPaged()
    {
        var specification = new Specification<Course>(c => true)
            .AddInclude(c => c.Include(c => c.InstructorsToCourses))
            .AddInclude(c => c.Include(c => c.Category))
            .ApplyOrderBy(q => q.OrderByDescending(c => c.CreatedAt));
        specification.ApplyPaging(1, 12);
        var popularCoursesPaged = await _UnitOfWork.Repository<Course>().FindBySpecificationAsync(specification);

        IList<CourseCardDto> popularCoursesPagedInDto = new List<CourseCardDto>(); 

        if (popularCoursesPagedInDto is not null)
        {
            foreach (var c in popularCoursesPaged)
            {
                var courseDto = new CourseCardDto()
                {
                    CourseId = c.CourseId,
                    CategoryName = c.Category?.CategoryName?? string.Empty,
                    CourseName = c.CourseName,
                    ThumbnailUrl = c.ThumbnailUrl,
                    Price = c.Price
                };
                popularCoursesPagedInDto.Add(courseDto); 
            }
        }

        return popularCoursesPagedInDto;
    }

    public async Task<IEnumerable<Course>> GetCoursesByCategory(ISpecification<Course> spec)
    {
        var CoursesBySpec = await _UnitOfWork.Repository<Course>().FindBySpecificationAsync(spec);
        
        return CoursesBySpec;
    }


    // TODO NOT COMPLETE YET
    public async Task<Course> AddCourse(AddCourseModel model)
    {
        if (model == null)
        {
            throw new ArgumentNullException(nameof(model));
        }

        var newCourse = model.ToEntity();
        
        await _UnitOfWork.Repository<Course>().AddAsync(newCourse);
        await _UnitOfWork.CompleteAsync();
        return newCourse;
    }
    public async Task<bool> DeleteCourseAsync(string id)
    {
        var course = await _UnitOfWork.Repository<Course>().FindAsync(c => c.CourseId == new Guid(id));
        var courseEntity = course.FirstOrDefault();
        
        if (courseEntity == null)
        {
            return false; // Course not found
        }

        await _UnitOfWork.Repository<Course>().DeleteAsync(courseEntity);
        await _UnitOfWork.CompleteAsync();
        return true; 
    }

}
