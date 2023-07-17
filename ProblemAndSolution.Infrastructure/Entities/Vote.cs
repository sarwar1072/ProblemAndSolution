using DevSkill.Data;
using ProblemAndSolution.Infrastructure.Entities.Membership;

namespace ProblemAndSolution.Infrastructure.Entities
{
    public class Vote : IEntity<int>
    {
        public int Id { get; set; }
        public Guid ApplicationUserId { get; set; }
        public ApplicationUser? ApplicationUser { get; set; }
        public int? QuestionId { get; set; }
        public int? AnswerId { get; set; }



    }
}
