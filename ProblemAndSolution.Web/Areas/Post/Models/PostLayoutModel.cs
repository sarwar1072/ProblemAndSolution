using AutoMapper;
using ProblemAndSolution.Membership.BusinessObj;
using ProblemAndSolution.Membership.Services;
using ProblemAndSolution.Web.Models;

namespace ProblemAndSolution.Web.Areas.Post.Models
{
    public class PostLayoutModel:BaseModel
    {
        public PostLayoutModel()
        {
        }
        public PostLayoutModel(IUserManagerAdapter<ApplicationUser> userManagerAdapter,IHttpContextAccessor httpContextAccessor,
            IMapper mapper):base(userManagerAdapter, httpContextAccessor,mapper)
        {

        }
    }
}
