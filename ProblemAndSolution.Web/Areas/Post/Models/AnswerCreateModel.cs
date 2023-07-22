using Autofac;
using AutoMapper;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using ProblemAndSolution.Infrastructure.BusinessObj;
using ProblemAndSolution.Infrastructure.Services;
using ProblemAndSolution.Membership.BusinessObj;
using ProblemAndSolution.Membership.Services;
using System.Runtime.CompilerServices;
using static System.Formats.Asn1.AsnWriter;

namespace ProblemAndSolution.Web.Areas.Post.Models
{
    public class AnswerCreateModel:PostLayoutModel
    {
        private IAnswerServices _answerServices;
        private ICommentService _commentService;

        public string? Description { get; set; }
        public string? AuthorName { get; set; }
        public int QuestionId { get; set; }
        public Guid? TempId { get; set; }
        public Question? Question { get; set; }
        public IList<Comment>? Comments { get; set; }

        public AnswerCreateModel()
        {

        }

        public AnswerCreateModel(IUserManagerAdapter<ApplicationUser> userManagerAdapter,
            IHttpContextAccessor httpContextAccessor, IMapper mapper,
            IAnswerServices answerService, ICommentService commentservice)
            : base(userManagerAdapter, httpContextAccessor, mapper)
        {
            _answerServices = answerService;
            _commentService = commentservice;
        }

        public override void ResolveDependency(ILifetimeScope lifetimeScope)
        {
            _lifetimeScope = lifetimeScope;
            _answerServices = _lifetimeScope.Resolve<IAnswerServices>();

            base.ResolveDependency(_lifetimeScope);
        }
        internal async Task AnswerAsync(string answerText,int questId)
        {
            await GetUserInfoAsync();

            var answer = new Answer
            {
                QuestionId=questId,
                Description= answerText,    
                AuthorName=basicInfo.FirstName,
                TempId=basicInfo.Id,

            };
            await _answerServices.CreateAnswerAsync(answer);
        }

        internal async Task CommentAsync(string commentVal,int answerId)
        {
            await GetUserInfoAsync();
            var comment = new Comment() {
                AuthorName = basicInfo!.FirstName,
                AnswerId = answerId,
                Description = commentVal,
                CreatedBy = basicInfo!.FirstName,
                DateTime = DateTime.UtcNow,
                TempId = basicInfo!.Id
            };
            await _commentService.CreateCommentAsync(comment);  
        }

        internal async Task<int> GetQuestionVote(int quesId)
        {
            await GetUserInfoAsync();

            var id = basicInfo!.Id;

            return await _commentService.GetQusnVote(id, quesId);
        }

        internal async Task<int> GetAnsVote(int answerId)
        {
            await GetUserInfoAsync();

            var id = basicInfo!.Id;

            return await _commentService.GetAnsVote(id, answerId);
        }



    }
}
