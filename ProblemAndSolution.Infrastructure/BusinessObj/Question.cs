using ProblemAndSolution.Infrastructure.BusinessObj.Membership;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemAndSolution.Infrastructure.BusinessObj
{
    public class Question
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? AuthorName { get; set; }
        public string? QuestionBody { get; set; }
        public DateTime  CreatedAt { get; set; }
        public int Temp1 { get; set; }
        public bool IsSolvedQsn { get; set; }
        public IList<Tag>? Tags { get; set; }
        public IList<Answer>? Answers{ get; set; }
        public Guid ApplicationUserId { get; set; }
        public ApplicationUser? ApplicationUser { get; set; }
    }
}
