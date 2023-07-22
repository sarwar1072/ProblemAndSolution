using Autofac;
using AutoMapper;
using ProblemAndSolution.Infrastructure.BusinessObj;
using ProblemAndSolution.Infrastructure.Services;
using ProblemAndSolution.Membership.BusinessObj;
using ProblemAndSolution.Membership.Services;
using System.ComponentModel.DataAnnotations;
using static System.Formats.Asn1.AsnWriter;

namespace ProblemAndSolution.Web.Areas.Post.Models
{
    public class QuestionEditModel:PostLayoutModel
    {
        private IQuestionServices _questionService;

        public int Id { get; set; }
        public Guid ApplicationUserId { get; set; }

        [Required]
        public string? Title { get; set; }
        public int? TotalQutnVote { get; set; }

        [Required]
        public string? QuestionBody { get; set; }
        public DateTime? CreatedAt { get; set; }
        public bool IsSolvedQstn { get; set; }
        public string? AuthorName { get; set; }
        public int Temp1 { get; set; }
        public int CountVote { get; set; }
        public ApplicationUser? ApplicationUser { get; set; }

        [Required]
        public IList<Tag>? Tags { get; set; }
        public IList<Answer>? Answers { get; set; }
        public IList<Question>? Questions { get; set; }

        public QuestionEditModel()
        {

        }

        public QuestionEditModel(IUserManagerAdapter<ApplicationUser> userManagerAdapter,
            IHttpContextAccessor httpContextAccessor, IMapper mapper, IQuestionServices questionService)
            : base(userManagerAdapter, httpContextAccessor, mapper)
        {
            _questionService = questionService;
        }

        public override void ResolveDependency(ILifetimeScope lifetimeScope)
        {
            _lifetimeScope = lifetimeScope;
            _questionService = _lifetimeScope.Resolve<IQuestionServices>();

            base.ResolveDependency(_lifetimeScope);
        }



    }
}
