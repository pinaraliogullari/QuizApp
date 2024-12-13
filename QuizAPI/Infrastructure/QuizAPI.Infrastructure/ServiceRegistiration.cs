using Microsoft.Extensions.DependencyInjection;
using QuizAPI.Application;
using QuizAPI.Application.Services.Redis;

namespace QuizAPI.Infrastructure;

public static class ServiceRegistiration
{
    public static void AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<ITokenHandler, TokenHandler>();
        services.AddScoped<ICacheService,ICacheService>();
    }
}
