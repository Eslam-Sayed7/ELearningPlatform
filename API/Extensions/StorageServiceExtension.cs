using Infrastructure.Data;
using Infrastructure.Data.IServices;
using Infrastructure.Data.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions;

public static class StorageServiceExtension
{
    public static void RegisterStorageService(this WebApplicationBuilder builder)
    {
        
        builder.Services.AddDbContext<AppDbContext>(x => x.UseNpgsql(Environment.GetEnvironmentVariable("POSTGRES_CONNECTION") ));
        builder.Services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = builder.Configuration.GetConnectionString("Redis");
            options.InstanceName = "RedisInstance";
        });
        
        builder.Services.AddScoped<IRedisCachService, RedisCacheService>();
    }
    
}