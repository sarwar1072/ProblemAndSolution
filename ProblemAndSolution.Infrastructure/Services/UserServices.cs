using UserProfileBO=ProblemAndSolution.Infrastructure.BusinessObj.UserProfile;
using ProblemAndSolution.Infrastructure.Exceptions;
using ProblemAndSolution.Infrastructure.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserProfileEO = ProblemAndSolution.Infrastructure.Entities.UserProfile;
using ProblemAndSolution.Infrastructure.BusinessObj;
using BlogBO = ProblemAndSolution.Infrastructure.BusinessObj.Blog;

namespace ProblemAndSolution.Infrastructure.Services
{
    public class UserServices:IUserServices
    {
        public IPAndSUnitOfWork _pAndSUnitOfWork;
        public UserServices(IPAndSUnitOfWork pAndSUnitOfWork)
        {
                _pAndSUnitOfWork = pAndSUnitOfWork;
        }

        public async Task AddOnlyId(UserProfileBO userProfile)
        {
            if(userProfile == null) {
                throw new InvalidOperationException("User id is null");
            }
            var count=await _pAndSUnitOfWork.UserProfileRepository.GetCountAsync(c=>c.ApplicationUserId==userProfile.ApplicationUserId);
            if(count!=0) {
                throw new DuplicateException("exist");
            }
            var entity=new UserProfileEO()
            {
                ProfileURL= userProfile.ProfileURL, 
                Profession=userProfile.Profession,  
                ApplicationUserId=userProfile.ApplicationUserId,
            };   
          await  _pAndSUnitOfWork.UserProfileRepository.AddAsync(entity);
          await  _pAndSUnitOfWork.SaveAsync();    

        }
        public async Task<UserSpecificBlogBO> UserSpecificBlog(Guid userId)
        {
            var blogList = (await _pAndSUnitOfWork.BlogRepository.GetAsync(a => a.PostId == userId, null)).ToList();

            var userProfile = (await _pAndSUnitOfWork.UserProfileRepository.
                GetAsync(x => x.ApplicationUserId == userId, b => b.Include(c => c.ApplicationUser))).FirstOrDefault();

            var model = new UserSpecificBlogBO();

            if(userProfile != null) 
            {
                model.ProfileURL = userProfile.ProfileURL;
                model.Profession= userProfile.Profession;
                model.UserName = userProfile.ApplicationUser?.FirstName;       
            }
            //model.Blogs = new List<Blog>();
            //foreach (var blog in blogList)
            //{
            //    model.Blogs.Add(new BlogBO
            //    {

            //    });
            //}  
            // Initialize and map the blogs to the model
           //var md=HtmlHelpers.SanitizeHtml          
            model.Blogs = blogList.Select(blog => new BlogBO
            {
                Id = blog.Id,
                Heading = blog.Heading,
                Content =HtmlHelpers.TruncateHtml( blog.Content,10),
                PublishedDate = blog.PublishedDate,
                Author = blog.Author,
                ImageUrl= blog.ImageUrl,    
            }).ToList();

            return model;   

        }
        

    }
    
}
