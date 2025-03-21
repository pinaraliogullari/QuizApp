using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using QuizAPI.Application.Behaviors;
using QuizAPI.Application.Validators;

namespace QuizAPI.Application;

public static class ServiceRegistiration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(typeof(ServiceRegistiration).Assembly);
            configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });
        services.AddValidatorsFromAssembly(typeof(LoginUserCommandRequestValidator).Assembly);
        return services;
    }
}
