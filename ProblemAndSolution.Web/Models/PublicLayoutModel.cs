using AutoMapper;
using ProblemAndSolution.Membership.BusinessObj;
using ProblemAndSolution.Membership.Services;

namespace ProblemAndSolution.Web.Models
{
    public class PublicLayoutModel:BaseModel
    {
        public PublicLayoutModel()
        {
        }
        public PublicLayoutModel(IUserManagerAdapter<ApplicationUser> userManagerAdapter,
            IHttpContextAccessor httpContextAccessor,IMapper mapper):base(userManagerAdapter,httpContextAccessor,mapper)
        {

        }
    }
}
