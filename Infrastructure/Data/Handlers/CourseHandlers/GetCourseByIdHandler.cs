using API.Extensions.Mappings;
using Infrastructure.Data.IServices;
using Infrastructure.Data.Queries.CourseQueries;
using Infrastructure.Dtos;
using MediatR;

namespace Infrastructure.Data.Handlers.CourseHandlers;

public class GetCourseByIdHandler(ICourseService courseService) : IRequestHandler<GetCourseByIdQuery, CourseCardDto>
{
    public async Task<CourseCardDto> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
    {
        var course = await courseService.GetCourseByIdAsync(request.CourseId);
        return course == null ? null : course.ToDto();
    }
}