using API.Extensions.Mappings;
using Infrastructure.Data.Commands.CourseCommands;
using Infrastructure.Data.Services;
using Infrastructure.Dtos;
using MediatR;

namespace Infrastructure.Data.Commands.StudentCommands;

public class CreateCourseHandler(CourseService courseService) : IRequestHandler<CreateCourseCommand, CourseCardDto>
{
    public async Task<CourseCardDto> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
        var createdCourse = await courseService.AddCourse(request.CourseModel);
        return createdCourse.ToDto();
    }
}