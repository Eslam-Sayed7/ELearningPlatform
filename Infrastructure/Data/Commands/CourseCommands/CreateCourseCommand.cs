using Infrastructure.Data.Models;
using Infrastructure.Dtos;
using MediatR;

namespace Infrastructure.Data.Commands.CourseCommands;

public class CreateCourseCommand : IRequest<CourseCardDto>
{
    public readonly AddCourseModel _courseModel;
    public CreateCourseCommand(AddCourseModel courseModel) 
    {
        _courseModel = courseModel ?? throw new ArgumentNullException(nameof(courseModel));
    }
}