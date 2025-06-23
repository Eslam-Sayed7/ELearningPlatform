using Core.Entities;
using Infrastructure.Data.Models;
using Infrastructure.Dtos;

namespace API.Extensions.Mappings;

public static class CourseDtoExtensions
{
    public static CourseCardDto ToDto(this Course course)
    {
        if (course == null) return null;

        var instructor = course.Instructors?.FirstOrDefault();
        var firstName = instructor?.Appuser?.FirstName ?? "";
        var lastName = instructor?.Appuser?.LastName ?? "";

        return new CourseCardDto() 
        {
            CourseId = course.CourseId,
            CourseName = course.CourseName,
            CourseDescription = course.Description,
            Level = course.Level,
            Price = course.Price,
            Duration = course.Duration,
            ThumbnailUrl = course.ThumbnailUrl,
            Language = course.Language,
            Instructor = $"{firstName} {lastName}"
        };
    }

    public static Course ToEntity(this AddCourseModel model)
    {
        return new Course 
        {
            CourseId = Guid.CreateVersion7(), 
            CourseName = model.CourseName,
            Description = model.Description,
            CategoryId = model.CategoryId, 
            Level = model.Level,
            Price = model.Price,
            Duration = model.Duration,
            ThumbnailUrl = model.ThumbnailUrl,
            Language = model.Language,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };
    }
}