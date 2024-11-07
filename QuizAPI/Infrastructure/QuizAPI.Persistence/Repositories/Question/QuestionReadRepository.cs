using QuizAPI.Application.Repositories;
using QuizAPI.Domain.Entities;
using QuizAPI.Persistence.Context;

namespace QuizAPI.Persistence.Repositories;

public class QuestionReadRepository : ReadRepository<Question>, IQuestionReadRepository
{
    public QuestionReadRepository(QuizAppDbContext context) : base(context)
    {
    }
}
