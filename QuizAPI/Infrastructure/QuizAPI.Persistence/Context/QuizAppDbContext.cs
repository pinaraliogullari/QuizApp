using Microsoft.EntityFrameworkCore;
using QuizAPI.Domain.Entities;
using System.Reflection;

namespace QuizAPI.Persistence.Context;

public class QuizAppDbContext : DbContext
{
    public QuizAppDbContext(DbContextOptions<QuizAppDbContext> options) : base(options)
    {
    }

    public DbSet<Question> Questions { get; set; }
    public DbSet<Participant> Participants { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}
