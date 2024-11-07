using QuizAPI.Application.Repositories;
using QuizAPI.Domain.Entities;
using QuizAPI.Persistence.Context;

namespace QuizAPI.Persistence.Repositories;

public class ParticipantReadRepository : ReadRepository<Participant>, IParticipantReadRepository
{
    public ParticipantReadRepository(QuizAppDbContext context) : base(context)
    {
    }
}
