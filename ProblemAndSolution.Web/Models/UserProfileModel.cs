using Autofac;
using AutoMapper;
using ProblemAndSolution.Infrastructure.BusinessObj;
using ProblemAndSolution.Infrastructure.Services;
using ProblemAndSolution.Membership.BusinessObj;
using ProblemAndSolution.Membership.Services;

namespace ProblemAndSolution.Web.Models
{
    public class UserProfileModel:BaseModel
    {
        private IUserServices _userServices;
        public string? ProfileURL { get; set; }
        public IFormFile? formFile { get; set; } 
        public string? Profession { get; set; }

        public Guid ApplicationUserId { get; set; }
        //public ApplicationUser ApplicationUser { get; set; }
        public UserProfileModel(IUserManagerAdapter<ApplicationUser> userManagerAdapter, IHttpContextAccessor contextAccessor,
           IMapper mapper,IUserServices userServices)
        {
                _userServices=userServices;
        }
        public UserProfileModel()
        {
                
        }
        public override void ResolveDependency(ILifetimeScope lifetimeScope)
        {
            _lifetimeScope = lifetimeScope;
            _userServices = _lifetimeScope.Resolve<IUserServices>();

            base.ResolveDependency(_lifetimeScope);
        }
       
        internal async Task AddProfile()
        {
            await GetUserInfoAsync();
            var model = new UserProfile()
            {
                ProfileURL = ProfileURL,
                Profession = Profession,
                ApplicationUserId = basicInfo!.Id,
            };

            await _userServices.AddOnlyId(model);
        }


    }
}
