using Microsoft.EntityFrameworkCore;
using ProblemAndSolution.Infrastructure.Entities;
using ProblemAndSolution.Infrastructure.Entities.Membership;

namespace ProblemAndSolution.Infrastructure.DbContexts
{
    public interface IApplicationDbContext
    {
        DbSet<ApplicationUser>? ApplicationUsers { get; set; }
        DbSet<Question>? Questions { get; set; }
        DbSet<Answer>? Answers { get; set; }
        DbSet<Comment>? Comments { get; set; }
        DbSet<Tag>? Tags { get; set; }
        DbSet<Vote>? Votes { get; set; }
        DbSet<BlogComment>? BlogsComment { get; set; }
        DbSet<Like> Likes { get; set; }
         DbSet<UserProfile> userProfiles { get; set; }

    }
}
