using Infrastructure.Data.Commands.CourseCommands;
using Infrastructure.Data.Queries.CourseQueries;
using Infrastructure.Data.Services;

namespace API.Extensions;

public static class MediatrExtensions
{
    
    public static void RegisterMediatrChannel(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(typeof(Program).Assembly);
            config.RegisterServicesFromAssembly(typeof(CreateCourseCommand).Assembly);
            config.RegisterServicesFromAssembly(typeof(GetCourseByIdQuery).Assembly);
            config.RegisterServicesFromAssembly(typeof(GetPopularCoursesQuery).Assembly);
        });
    }
    
}