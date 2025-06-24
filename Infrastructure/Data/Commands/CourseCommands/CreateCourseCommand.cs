using Infrastructure.Data.Models;
using Infrastructure.Dtos;
using MediatR;

namespace Infrastructure.Data.Commands.CourseCommands;

public class CreateCourseCommand(AddCourseModel courseModel) : IRequest<CourseCardDto>
{
    public readonly AddCourseModel CourseModel = courseModel;
}