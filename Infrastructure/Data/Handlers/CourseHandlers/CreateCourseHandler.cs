using API.Extensions.Mappings;
using Infrastructure.Data.Commands.CourseCommands;
using Infrastructure.Data.IServices;
using Infrastructure.Dtos;
using MediatR;

namespace Infrastructure.Data.Handlers.CourseHandlers;

public class CreateCourseHandler(ICourseService courseService) : IRequestHandler<CreateCourseCommand, CourseCardDto>
{
    public async Task<CourseCardDto> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
        var createdCourse = await courseService.AddCourse(request._courseModel);
        return createdCourse.ToDto();
    }
}