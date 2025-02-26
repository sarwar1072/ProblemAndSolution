﻿using Autofac;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using ProblemAndSolution.Infrastructure;
using ProblemAndSolution.Infrastructure.BusinessObj;
using ProblemAndSolution.Infrastructure.Services;
using ProblemAndSolution.Membership.BusinessObj;
using ProblemAndSolution.Membership.Services;

namespace ProblemAndSolution.Web.Models
{
    public class UserProfileViewModel:BaseModel
    {
        public  IUserServices _userServices;  
        public List<Blog>? Blogs { get; set; }
        public string? ProfileURL { get; set; }
        public string? UserName { get; set; }
        public string? Profession { get; set; }
        public Guid ApplicationUserId { get; set; }
        //public ApplicationUser ApplicationUser { get; set; }
        public UserProfileViewModel(IUserManagerAdapter<ApplicationUser> userManagerAdapter, IHttpContextAccessor contextAccessor,
           IMapper mapper, IUserServices userServices):base(userManagerAdapter, contextAccessor, mapper)
        {
            _userServices = userServices;
        }
        public UserProfileViewModel() { }     
        public override void ResolveDependency(ILifetimeScope lifetimeScope)
        {
            _lifetimeScope = lifetimeScope;
            _userServices = _lifetimeScope.Resolve<IUserServices>();
            base.ResolveDependency(_lifetimeScope);
        }
        internal async Task GetUserProfile(Guid userId)
        {
           var data = await _userServices.UserSpecificBlog(userId);
          
           var userData= await UserData(userId);
            if (data != null)
            {
                ProfileURL = data.ProfileURL;
                Profession = data.Profession;
                UserName = userData.FirstName;
                Blogs = new List<Blog>();

                if(data.Blogs != null) { 
                   foreach (var blog in data.Blogs)
                    {
                        Blogs.Add(blog);
                    }
                }
                //Blogs = data.Select(blog => new Blog
                //{
                //    Id = blog.Id,
                //    Heading = blog.Heading,
                //    Content = blog.Content,
                //    PublishedDate = blog.PublishedDate,
                //    Author = blog.Author,
                //    ImageUrl = blog.ImageUrl,
                //}).ToList();
            }
        }
        
        private  async Task<ApplicationUser> UserData(Guid id)
        {
            var user = await _userManagerAdapter!.GetById(id);
            var businessUser = new ApplicationUser()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
            };
            return businessUser;
        }


    }
}



