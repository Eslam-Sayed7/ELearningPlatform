using API.Extensions;
using API.Middlewares;
using DotNetEnv;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
Env.Load(".env");

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.RegisterSecurityServices();
builder.Host.ConfigureLogging();
builder.RegisterStorageService();
builder.RegisterServices();
builder.Services.AddHttpContextAccessor();
builder.Services.AddIdentityServices();
builder.Services.RegisterMediatrChannel();
builder.Services.AddControllers();
builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// app.MapIdentityApi<AppUser>();
// app.ApplyMigrations();

app.UseHttpsRedirection();
app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
app.UseSerilogRequestLogging();
app.UseStaticFiles();
app.UseDefaultFiles();

app.UseAuthentication();  
app.UseAuthorization();

app.MapControllers();

app.Run();

