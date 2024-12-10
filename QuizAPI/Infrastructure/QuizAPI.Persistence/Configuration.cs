using Microsoft.Extensions.Configuration;

namespace QuizAPI.Persistence;

public static class Configuration
{
    public static string GetConnectionString()
    {
        ConfigurationManager configurationManager = new();
        configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../Presentation/QuizAPI.API"));
        configurationManager.AddJsonFile("appsettings.json");
        return configurationManager.GetConnectionString("SqlConnection");
    }
}
