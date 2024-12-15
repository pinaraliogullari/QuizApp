namespace QuizAPI.Application.Services.Redis;

public interface ICacheService
{
    Task SetAsync<T>(string key, T value, TimeSpan absoluteExpirationRelativeToNow, TimeSpan slidingExpiration = default);
    Task<T> GetAsync<T>(string key);
    Task RemoveAsync(string key);
}
