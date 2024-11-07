using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace QuizAPI.Application;

public static class ServiceRegistiration
{
    public static void AddApplicationService(this IServiceCollection services)
    {
        services.AddMediatR(typeof(ServiceRegistiration));
    }
}
