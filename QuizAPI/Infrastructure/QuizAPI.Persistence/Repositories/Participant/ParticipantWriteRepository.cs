using QuizAPI.Application.Repositories;
using QuizAPI.Domain.Entities;
using QuizAPI.Persistence.Context;

namespace QuizAPI.Persistence.Repositories
{
    public class ParticipantWriteRepository : WriteRepository<Participant>, IParticipantWriteRepository
    {
        public ParticipantWriteRepository(QuizAppDbContext context) : base(context)
        {
        }
    }
}
