using API.Extensions;
using API.Extensions.Migrations;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Core.Entities;
using Infrastructure.Services.Auth;
using Infrastructure.Data.IServices;
using Infrastructure.Data.Services;
using Infrastructure.Services.Enrollservice;
using Infrastructure.Base;
using DotNetEnv;
using Serilog;
// using Infrastructure.Services.Pay;
//using Infrastructure.Services.Pay;

var builder = WebApplication.CreateBuilder(args);
Env.Load();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));
builder.ConfigureLogging();

builder.Services.AddDbContext<AppDbContext>(x =>
    x.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection")));
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    options.InstanceName = "RedisInstance";
});

builder.Services.AddHttpContextAccessor();

builder.Services.AddIdentity<AppUser , IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<AppDbContext>()
    .AddRoles<IdentityRole>()
    .AddApiEndpoints();

builder.Services.AddScoped<IAuthService , AuthService>();
builder.Services.AddTransient<IStudentService , StudentService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IEnrollmentService,EnrollmentServices>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddTransient<ICourseService, CourseService>();
//builder.Services.AddScoped<IPaymentService,PaymentService>();
builder.Services.AddScoped<IRedisCachService, RedisCacheService>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = false;
    options.TokenValidationParameters = new TokenValidationParameters {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidAudience =  builder.Configuration["JWT:Audience"],
        IssuerSigningKey  = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireRole("Admin")); 
    options.AddPolicy("Student", policy => policy.RequireRole("Student"));
    options.AddPolicy("Instructor", policy => policy.RequireRole("Instructor"));
});

// builder.Services.AddIdentityServices();
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// app.MapIdentityApi<AppUser>();
// app.ApplyMigrations();

app.UseHttpsRedirection();
// app.UseSerilogRequestLogging();
app.UseStaticFiles();
app.UseDefaultFiles();

app.UseAuthentication();  
app.UseAuthorization();

app.MapControllers();

app.Run();

