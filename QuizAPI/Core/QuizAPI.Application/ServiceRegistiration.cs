﻿using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace QuizAPI.Application;

public static class ServiceRegistiration
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(typeof(ServiceRegistiration));
    }
}
