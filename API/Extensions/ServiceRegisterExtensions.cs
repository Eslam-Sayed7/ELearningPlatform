using Infrastructure.Base;
using Infrastructure.Data.IServices;
using Infrastructure.Data.Services;
using Infrastructure.Services.Auth;
using Infrastructure.Services.Enrollservice;

namespace API.Extensions;

public static class ServiceRegisterExtensions
{
    public static void RegisterServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddTransient<IStudentService , StudentService>();
        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddScoped<IEnrollmentService,EnrollmentServices>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddTransient<ICourseService, CourseService>();
        //builder.Services.AddScoped<IPaymentService,PaymentService>();
    }
}