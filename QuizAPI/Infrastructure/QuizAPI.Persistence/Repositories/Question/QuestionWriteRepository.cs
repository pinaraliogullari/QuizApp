using QuizAPI.Application.Repositories;
using QuizAPI.Domain.Entities;
using QuizAPI.Persistence.Context;

namespace QuizAPI.Persistence.Repositories;

public class QuestionWriteRepository : WriteRepository<Question>, IQuestionWriteRepository
{
    public QuestionWriteRepository(QuizAppDbContext context) : base(context)
    {
    }
}
