using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using QuizAPI.Persistence.Context;

namespace QuizAPI.Persistence;

public static class ServiceRegistiration
{
    public static void AddPersistenceServices(this IServiceCollection services)
    {
        services.AddDbContext<QuizAppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString()));
    }
}
