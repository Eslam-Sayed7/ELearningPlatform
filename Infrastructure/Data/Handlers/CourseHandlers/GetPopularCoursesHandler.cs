using Infrastructure.Data.IServices;
using Infrastructure.Data.Queries.CourseQueries;
using Infrastructure.Dtos;
using MediatR;

namespace Infrastructure.Data.Handlers.CourseHandlers;

public class GetPopularCoursesHandler(IRedisCachService cache, ICourseService courseService)
    : IRequestHandler<GetPopularCoursesQuery, IEnumerable<CourseCardDto>>
{
    public async Task<IEnumerable<CourseCardDto>> Handle(GetPopularCoursesQuery request, CancellationToken cancellationToken = default)
    {
        var courses = cache.GetData<IEnumerable<CourseCardDto>>("PopularCourses");
        if(courses is not null)
        {
            return courses;
        } 
        courses = await courseService.GetPopularCoursesPaged();
        cache.SetData("PopularCourses" , courses);
        return courses;
    }
}