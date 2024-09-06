using Microsoft.AspNetCore.Identity;

namespace ProblemAndSolution.Infrastructure.Entities.Membership
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public UserProfile? ProfileUrl {  get; set; } 
        public IList<Question>? Questions { get; set; }
    }
}
