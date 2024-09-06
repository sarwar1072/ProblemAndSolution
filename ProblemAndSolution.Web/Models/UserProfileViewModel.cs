using Autofac;
using AutoMapper;
using ProblemAndSolution.Infrastructure.Entities;
using ProblemAndSolution.Infrastructure.Services;
using ProblemAndSolution.Membership.BusinessObj;
using ProblemAndSolution.Membership.Services;

namespace ProblemAndSolution.Web.Models
{
    public class UserProfileViewModel:BaseModel
    {
        private IUserServices _userServices;
        public string? ProfileURL { get; set; }
        public string? Profession { get; set; }
        public IList<Blog> blog { get; set; }  
        public UserProfile UserProfile { get; set; }    
        public Guid ApplicationUserId { get; set; }
        //public ApplicationUser ApplicationUser { get; set; }
        public UserProfileViewModel(IUserManagerAdapter<ApplicationUser> userManagerAdapter, IHttpContextAccessor contextAccessor,
           IMapper mapper, IUserServices userServices)
        {
            _userServices = userServices;
        }
        public UserProfileViewModel()
        {

        }
        public override void ResolveDependency(ILifetimeScope lifetimeScope)
        {
            _lifetimeScope = lifetimeScope;
            _userServices = _lifetimeScope.Resolve<IUserServices>();

            base.ResolveDependency(_lifetimeScope);
        }

        internal async Task GetUserProfile(Guid userId)
        {
            await _userServices.UserSpecificBlog(userId);

            //if(data != null)
            //{


            //}

        }
    }
}
