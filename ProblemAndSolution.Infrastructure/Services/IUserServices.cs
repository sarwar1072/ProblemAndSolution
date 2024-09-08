using UserProfile = ProblemAndSolution.Infrastructure.Entities.UserProfile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserProfileBO=ProblemAndSolution.Infrastructure.BusinessObj.UserProfile;
using ProblemAndSolution.Infrastructure.BusinessObj;

namespace ProblemAndSolution.Infrastructure.Services
{
    public interface IUserServices
    {
        Task AddOnlyId(UserProfileBO userProfile);
        Task<UserSpecificBlogBO> UserSpecificBlog(Guid userId);
    }
}


   
