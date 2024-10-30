using QuizAPI.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
