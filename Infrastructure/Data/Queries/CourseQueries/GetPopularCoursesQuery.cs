using Infrastructure.Dtos;
using MediatR;

namespace Infrastructure.Data.Queries.CourseQueries;

public class GetPopularCoursesQuery : IRequest<IEnumerable<CourseCardDto>>
{
}