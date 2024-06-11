using AutoMapper;
using ProblemAndSolution.Infrastructure.BusinessObj;
using ProblemAndSolution.Infrastructure.Services;
using ProblemAndSolution.Membership.BusinessObj;
using ProblemAndSolution.Membership.Services;
using BlogBO = ProblemAndSolution.Infrastructure.BusinessObj.Blog;

namespace ProblemAndSolution.Web.Models
{
    public class PublicLayoutModel:BaseModel
    {
        private IQuestionServices _questionServices;
        private IBlogServices _blogServices;
        public PublicLayoutModel()
        {
        }
        public PublicLayoutModel(IUserManagerAdapter<ApplicationUser> userManagerAdapter, IBlogServices blogServices,
            IHttpContextAccessor httpContextAccessor, IQuestionServices questionServices, IMapper mapper):base(userManagerAdapter,httpContextAccessor,mapper)
        {
            _questionServices = questionServices;
            _blogServices = blogServices;   
        }
        internal async Task<List<Question?>?> GetQuestions(int index)
        {
            var questions = await _questionServices.GetPaginatedQuestions(index, 4);
            return questions;
        }
       
    }
}
