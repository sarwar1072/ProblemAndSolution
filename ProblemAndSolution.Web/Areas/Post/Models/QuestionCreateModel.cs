using Autofac;
using AutoMapper;
using ProblemAndSolution.Infrastructure.BusinessObj;
using ProblemAndSolution.Infrastructure.Services;
using ProblemAndSolution.Membership.BusinessObj;
using ProblemAndSolution.Membership.Services;

namespace ProblemAndSolution.Web.Areas.Post.Models
{
    public class QuestionCreateModel:PostLayoutModel
    {
        private IQuestionServices _questionServices;
        public Guid ApplicationUserId { get; set; }
       public ApplicationUser? ApplicationUser { get; set; }
        public string? Title { get; set; }
        public int? TotalQutnVote { get; set; }
        public string? QuestionBody { get; set; }
        public string? AuthorName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool IsSolvedQuestion { get; set; }

        public IList<string>? Tags { get; set; }
        public IList<Answer>? Answers { get; set; }

        public QuestionCreateModel()
        {

        }
        public QuestionCreateModel(IUserManagerAdapter<ApplicationUser> userManagerAdapter,IHttpContextAccessor httpContextAccessor
            ,IMapper mapper,IQuestionServices questionServices):base(userManagerAdapter,httpContextAccessor,mapper)
        {
            _questionServices = questionServices;
        }

        public override void ResolveDependency(ILifetimeScope lifetimeScope)
        {
            _lifetimeScope=lifetimeScope;   
            _questionServices=_lifetimeScope.Resolve<IQuestionServices>();
            base.ResolveDependency(lifetimeScope);
        }
    }
}
