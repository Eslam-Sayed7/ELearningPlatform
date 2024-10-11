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

    public async Task<GetCourseDto> GetCourseByIdAsync(Guid Id)
    {
        var searchcourse = await _UnitOfWork.Repository<Course>()
            .FindAsync(c => c.CourseId.ToString().ToUpper() == Id.ToString().ToUpper() , 
                include: q => q.Include( I => I.InstructorsToCourses)
                    .Include(c => c.Category));
        
        var course = searchcourse.FirstOrDefault();
        var instructorName = $"{course.Instructors.FirstOrDefault().Appuser.FirstName }{course.Instructors.FirstOrDefault().Appuser.LastName}";
        return new GetCourseDto()
        {
            CourseId = course.CourseId,
            CourseName = course.CourseName,
            Description = course.Description,
            Instructor = instructorName ,
            Level = course.Level,
            CategoryName = course.Category.CategoryName,
            ThumbnailUrl = course.ThumbnailUrl,
            Price = course.Price,
            Duration = course.Duration,
            Language = course.Language
        };
    }

    public async Task<IList<CourseCardDto>> GetPopularCoursesPaged()
    {
        var specification = new Specification<Course>(c => true)
            .AddInclude(c => c.Include(c => c.InstructorsToCourses))
            .AddInclude(c => c.Include(c => c.Category))
            .ApplyOrderBy(q => q.OrderByDescending(c => c.CreatedAt));
        specification.ApplyPaging(1, 12);
        var popularCoursesPaged = await _UnitOfWork.Repository<Course>().FindBySpecificationAsync(specification);
    
        IList<CourseCardDto> popularCoursesPagedInDto = new List<CourseCardDto>();  // Corrected initialization

        foreach (var c in popularCoursesPaged)
        {
            var courseDto = new CourseCardDto()
            {
                CourseId = c.CourseId,
                CategoryName = c.Category.CategoryName,
                CourseName = c.CourseName,
                ThumbnailUrl = c.ThumbnailUrl,
                Price = c.Price
            };
            popularCoursesPagedInDto.Add(courseDto);  // Use Add instead of Append
        }

        return popularCoursesPagedInDto;
    }

    public async Task<IEnumerable<Course>> GetCoursesByCategory(ISpecification<Course> spec)
    {
        var CoursesBySpec = await _UnitOfWork.Repository<Course>().FindBySpecificationAsync(spec);
        
        return CoursesBySpec;
    }

    

    //
    // public async Task<Course> AddCourse(AddCourseModel model)
    // {   
    //         var NewCourse = new Course
    //         {
    //             CourseName = model.CourseName,
    //             Description = model.Description,
    //             CategoryId = model.CategoryId,
    //             Level = model.Level,
    //             Price = model.Price,
    //             Duration = model.Duration,
    //             ThumbnailUrl = model.ThumbnailUrl,
    //             Language = model.Language,
    //             CreatedAt = DateTime.UtcNow,
    //             UpdatedAt = DateTime.UtcNow
    //         };
    //         IEnumerable<CourseSection> courseSections = new List<CourseSection>();
    //         
    //         foreach (var sectionModel in model.Sections)
    //         {
    //             sectionModel.SectionSequence = ++NewCourse.LastSectionSequence; // very important 
    //             var courseSection = new CourseSection
    //             {
    //                 SectionId = Guid.NewGuid(),
    //                 CourseId = NewCourse.CourseId,
    //                 Title = sectionModel.Title,
    //                 SectionSequence = sectionModel.SectionSequence
    //             };
    //             courseSections.Append(courseSection);
    //             
    //             foreach (var materialModel in sectionModel.Materials)
    //             {
    //                 var courseMaterial = new CourseMaterial
    //                 {
    //                     MaterialId = Guid.NewGuid(),
    //                     SectionId = courseSection.SectionId,
    //                     MaterialType = materialModel.MaterialType,
    //                     TextContent = materialModel.TextContent,
    //                     Url = materialModel.Url,
    //                     MaterialSequence = sectionModel.last
    //                 };
    //                 courseSection.CourseMaterials.Add(courseMaterial);  // Attach materials to the section
    //             }
    //
    //             _UnitOfWork.Repository<Course>().AddAsync(NewCourse);
    //             _UnitOfWork.Repository<CourseSection>().AddAsync(courseSection);
    //             NewCourse.CourseSections.Add(courseSection);  // Attach section to the course
    //         }
    //
    //         await _UnitOfWork.Repository<Course>().AddAsync(NewCourse);
    //
    //         await _UnitOfWork.CompleteAsync();
    //         return NewCourse;
    // }
    //
    // public async Task<> AddSection()
    // {
    //     
    // }
    //
    // public async 
    
}
