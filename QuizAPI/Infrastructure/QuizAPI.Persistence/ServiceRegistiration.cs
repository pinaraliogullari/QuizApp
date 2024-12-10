using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using QuizAPI.Application.CustomValidation;
using QuizAPI.Application.Repositories;
using QuizAPI.Domain.Entities;
using QuizAPI.Persistence.Context;
using QuizAPI.Persistence.Repositories;

namespace QuizAPI.Persistence;

public static class ServiceRegistiration
{
    public static void AddPersistenceServices(this IServiceCollection services)
    {
        services.AddDbContext<QuizAppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString()));
        services.AddIdentity<AppUser, AppRole>(options =>
        {
            options.Password.RequiredLength = 6;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.User.RequireUniqueEmail = true;
        }).AddPasswordValidator<CustomPasswordValidation>()
          .AddErrorDescriber<CustomIdentityErrorDescriber>()
          .AddEntityFrameworkStores<QuizAppDbContext>();

        services.ConfigureApplicationCookie(options =>
        {
            options.LogoutPath = "/";
        });

        services.AddScoped<IQuestionWriteRepository, QuestionWriteRepository>();
        services.AddScoped<IQuestionReadRepository, QuestionReadRepository>();
    }
}
