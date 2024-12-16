using Microsoft.Extensions.Caching.Distributed;
using QuizAPI.Application.Services.Redis;
using System.Text.Json;

namespace QuizAPI.Infrastructure.Services.Redis;

public class CacheService : ICacheService
{
    private readonly IDistributedCache _distributedCache;

    public CacheService(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }

    public async Task<T> GetAsync<T>(string key)
    {
        var jsonData= await _distributedCache.GetStringAsync(key);
        if (jsonData == null)
            return default;
        return JsonSerializer.Deserialize<T>(jsonData);
    }

    public async Task RemoveAsync(string key)=> await _distributedCache.RemoveAsync(key);

    public async Task SetAsync<T>(string key, T value, TimeSpan absoluteExpirationRelativeToNow, TimeSpan slidingExpiration = default)
    {
        var options = new DistributedCacheEntryOptions()
        {
            AbsoluteExpirationRelativeToNow = absoluteExpirationRelativeToNow,
            SlidingExpiration = slidingExpiration != null ? slidingExpiration : null,
        };
        var jsonData = JsonSerializer.Serialize(value);
        await _distributedCache.SetStringAsync(key,jsonData,options);
    }
}
