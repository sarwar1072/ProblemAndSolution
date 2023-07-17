using DevSkill.Data;

namespace ProblemAndSolution.Infrastructure.Entities
{
    public class Tag : IEntity<int>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int QuestionId { get; set; }
        public Question? Question { get; set; }
    }
}
