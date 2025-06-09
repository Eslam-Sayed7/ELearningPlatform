using System.Text.Json;
using Infrastructure.Data.IServices;
using Microsoft.Extensions.Caching.Distributed;

namespace Infrastructure.Data.Services;

public class RedisCacheService : IRedisCachService
{
    private readonly IDistributedCache _cache;

    public RedisCacheService(IDistributedCache cache)
    {
        _cache = cache;
    }

    public T? GetData<T>(string key)
    {
        var data = _cache.GetString(key);
        if (data == null) return default;
        return JsonSerializer.Deserialize<T>(data);
    }

    public void SetData<T>(string key, T data)
    {
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(60)  // one hour
        };
        var jsonData = JsonSerializer.Serialize(data);
        _cache?.SetString(key, jsonData, options);
    }
}