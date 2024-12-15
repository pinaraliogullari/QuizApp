using QuizAPI.Application.Services.Redis;

namespace QuizAPI.Infrastructure.Services.Redis;

public class CacheService : ICacheService
{
    public Task<T> GetAsync<T>(string key)
    {
        throw new NotImplementedException();
    }

    public Task RemoveAsync(string key)
    {
        throw new NotImplementedException();
    }

    public Task SetAsync<T>(string key, T value, TimeSpan absoluteExpirationRelativeToNow, TimeSpan slidingExpiration = default)
    {
        throw new NotImplementedException();
    }
}
