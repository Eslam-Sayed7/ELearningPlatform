using Infrastructure.Data;
using Infrastructure.Data.IServices;
using Infrastructure.Data.Services;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace API.Extensions;

public static class StorageServiceExtension
{
    public static void RegisterStorageService(this WebApplicationBuilder builder)
    {
        
        builder.Services.AddDbContext<AppDbContext>(x => x.UseNpgsql(Environment.GetEnvironmentVariable("POSTGRES_CONNECTION") ));
        builder.Services.AddStackExchangeRedisCache(options =>
        {
            options.ConfigurationOptions = new ConfigurationOptions
            {
                EndPoints = { builder.Configuration.GetConnectionString("Redis") },
                AbortOnConnectFail = false,
                ConnectRetry = 5,
                ConnectTimeout = 15000,
                SyncTimeout = 15000,
            };
            options.InstanceName = "RedisInstance";
        });

        builder.Services.AddSingleton<IRedisCachService, RedisCacheService>();
    }
    
}