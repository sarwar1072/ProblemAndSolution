using UserProfile=ProblemAndSolution.Infrastructure.Entities.UserProfile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProblemAndSolution.Infrastructure.Entities;

namespace ProblemAndSolution.Infrastructure.Services
{
    public interface IUserServices
    {
        Task AddOnlyId(UserProfile userProfile);
        Task<UserSpecificBlogBO> UserSpecificBlog(Guid userId);
    }
}
