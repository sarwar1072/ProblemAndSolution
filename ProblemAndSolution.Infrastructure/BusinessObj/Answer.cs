using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemAndSolution.Infrastructure.BusinessObj
{
    public class Answer
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public string?  AuthorName { get; set; }
        public int CountVote { get; set; }
        public IList<Comment>? Comments { get; set; }
        public Question? Question { get; set; }
        public Guid? TempId { get; set; }
        public int QuestionId { get; set; }

    }
}
