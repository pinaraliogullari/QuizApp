using Microsoft.Extensions.DependencyInjection;
using QuizAPI.Application;

namespace QuizAPI.Infrastructure;

public static class ServiceRegistiration
{
    public static void AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<ITokenHandler, TokenHandler>();
    }
}
