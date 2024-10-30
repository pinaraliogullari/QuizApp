using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizAPI.Domain.Entities;

namespace QuizAPI.Persistence.Configurations;

public class QuestionConfig : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder.Property(x => x.InWords).IsRequired().HasMaxLength(250);
        builder.Property(x => x.ImageName).HasMaxLength(50);
        builder.Property(x => x.Option1).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Option2).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Option3).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Option4).IsRequired().HasMaxLength(50);
    }
}
