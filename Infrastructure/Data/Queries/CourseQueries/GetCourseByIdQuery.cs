using Infrastructure.Dtos;
using MediatR;
using MediatR.Pipeline;

namespace Infrastructure.Data.Queries.CourseQueries;

public class GetCourseByIdQuery : IRequest<CourseCardDto>
{
    public Guid CourseId { get; set; }

    public GetCourseByIdQuery(Guid courseId)
    {
        CourseId = courseId;
    }
}