using UserProfile = ProblemAndSolution.Infrastructure.Entities.UserProfile;
using ProblemAndSolution.Infrastructure.Exceptions;
using ProblemAndSolution.Infrastructure.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProblemAndSolution.Infrastructure.Entities;

namespace ProblemAndSolution.Infrastructure.Services
{
    public class UserServices:IUserServices
    {
        public IPAndSUnitOfWork _pAndSUnitOfWork;
        public UserServices(IPAndSUnitOfWork pAndSUnitOfWork)
        {
                _pAndSUnitOfWork = pAndSUnitOfWork;
        }

        public async Task AddOnlyId(UserProfile userProfile)
        {
            if(userProfile == null) {
                throw new InvalidOperationException("User id is null");
            }
            var count=await _pAndSUnitOfWork.UserProfileRepository.GetCountAsync(c=>c.ApplicationUserId==userProfile.ApplicationUserId);
            if(count!=0) {
                throw new DuplicateException("exist");
            }
          await  _pAndSUnitOfWork.UserProfileRepository.AddAsync(userProfile);
          await  _pAndSUnitOfWork.SaveAsync();    

        }
        public async Task<UserSpecificBlogBO> UserSpecificBlog(Guid userId)
        {
            var blog = (await _pAndSUnitOfWork.BlogRepository.GetAsync(a => a.PostId == userId, null)).ToList();  
            
            var userProfile=(await _pAndSUnitOfWork.UserProfileRepository.
                GetAsync(x=>x.ApplicationUserId == userId,b=>b.Include(c=>c.ApplicationUser))).FirstOrDefault();

            var model = new UserSpecificBlogBO
            {
                Blog = blog,
                Profile = userProfile
            };
            return model;   
        }

    }
}
