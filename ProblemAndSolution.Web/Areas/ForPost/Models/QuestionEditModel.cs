using Autofac;
using AutoMapper;
using NuGet.Protocol.Plugins;
using ProblemAndSolution.Infrastructure.BusinessObj;
using ProblemAndSolution.Infrastructure.Services;
using ProblemAndSolution.Membership.BusinessObj;
using ProblemAndSolution.Membership.Services;
using System.ComponentModel.DataAnnotations;
using static System.Formats.Asn1.AsnWriter;

namespace ProblemAndSolution.Web.Areas.ForPost.Models
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

        internal async Task GetByIdAsync(int id)
        {
            if(id != 0)
            {
                var question = await _questionService.GetByIdAsync(id);
                if(question != null)
                {
                    Id= question.Id;
                    ApplicationUserId = question.ApplicationUserId;
                    Title = question.Title;
                    CreatedAt = question.CreatedAt;
                    IsSolvedQstn = question.IsSolvedQsn;
                    AuthorName = question.AuthorName;
                    QuestionBody = question.QuestionBody;
                    Tags = new List<Tag>();

                    if(question.Tags != null)
                    {
                        foreach (var item in question.Tags)
                        {
                            Tags.Add(new Tag
                            {
                                Name=item.Name,
                                Id=item.Id,
                                QuestionId=item.QuestionId
                            });
                        }
                    }
                }
            }
        }

        private Question MapQuestion()
        {
            var question = new Question
            {
                Id = Id,
                ApplicationUserId=basicInfo.Id,
                CreatedAt=DateTime.UtcNow,
                Title=Title,
                QuestionBody=QuestionBody,
                IsSolvedQsn=false,
            };
            question.Tags = new List<Tag>();
            if (Tags.Count() > 0)
            {
                foreach (var item in Tags)
                {
                    question.Tags.Add(new Tag { 
                     Id=item.Id,
                     Name=item.Name,    
                     QuestionId=item.QuestionId
                    });
                }
            }
            return question;
        }

        internal async Task UpdateAsync()
        {
            var question = MapQuestion();
            await _questionService.UpdateQuestionAsync(question);   
        }

        internal async Task DeleteQuestionAsync(int id)
        {
            await _questionService.DeleteQuestionAsync(id);
        }

        internal async Task GetUserSpecificPost()
        {
            await GetUserInfoAsync();
            var user = basicInfo!.Id;
            var questions = await _questionService.GetQuestionsAsync(user);
            Questions = new List<Question>();
            foreach (var item in questions)
            {
                Questions.Add(item);
            }
        }

        internal void GetTemp()
        {
            _questionService.GetTest(1);
        }

        internal async Task Details(int id)
        {
            var question = await _questionService.GetDetails(id);
            if(question != null)
            {
                Id= question.Id;    
                ApplicationUserId= question.ApplicationUserId;  
                Title=question.Title;
                CreatedAt = question.CreatedAt;
                QuestionBody=question.QuestionBody;
                Temp1= question.Temp1;
                AuthorName = question.AuthorName;
                Tags = new List<Tag>();
                Answers = new List<Answer>();

                if (question.Tags != null)
                {
                    foreach (var item in question.Tags)
                    {
                        Tags.Add(new Tag
                        {
                            Id=item.Id,
                            Name=item.Name,
                            QuestionId=item.QuestionId
                        });
                    }
                }
                if (question.Answers != null)
                {
                    foreach (var answer in question.Answers)
                    {
                        var comment = new List<Comment>();
                        if (answer.Comments != null)
                        {
                            foreach (var com in answer.Comments)
                            {
                                comment.Add(new Comment
                                {
                                    Description=com.Description,
                                    DateTime=com.DateTime,
                                    AnswerId=com.AnswerId,
                                    AuthorName=com.AuthorName,
                                    TempId=com.TempId,
                                    CreatedBy=com.CreatedBy,
                                });
                            }
                        }
                        Answers.Add(new Answer
                        {
                            Description = answer.Description,
                            Id = answer.Id,
                            AuthorName = answer.AuthorName,
                            CountVote = answer.CountVote,
                            Comments = comment
                        });
                    }
                }
            }
        }


    }
}
