using Microsoft.AspNetCore.Identity;

namespace QuizAPI.Domain.Entities
{
    public class AppUser:IdentityUser<string>
    {
        public int Score { get; set; }
        public int TimeTaken { get; set; }
    }
}
