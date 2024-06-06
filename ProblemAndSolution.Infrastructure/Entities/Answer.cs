using DevSkill.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProblemAndSolution.Infrastructure.Entities
{
    public class Answer : IEntity<int>
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public string? AuthorName { get; set; }
        public int QuestionId { get; set; }
        public Guid? TempId { get; set; }
        [NotMapped]
        public int CountVote { get; set; }
        public Question? Question { get; set; }
        public IList<Comment>? Comments { get; set; }
    }
}
