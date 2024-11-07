using QuizAPI.Domain.Common;

namespace QuizAPI.Domain.Entities
{
    public class Participant : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public int Score { get; set; }
        public int TimeTaken { get; set; }
    }
}
