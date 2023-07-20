using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuestionEO = ProblemAndSolution.Infrastructure.Entities.Question;
using AnswerEO = ProblemAndSolution.Infrastructure.Entities.Answer;
using CommentEO = ProblemAndSolution.Infrastructure.Entities.Comment;
using ProblemAndSolution.Infrastructure.BusinessObj;

namespace ProblemAndSolution.Infrastructure.Profiles
{
    public class InfrastructureProfile:Profile
    {
        public InfrastructureProfile()
        {
            CreateMap<Question, QuestionEO>().ReverseMap();
            CreateMap<Answer, AnswerEO>().ReverseMap();
            CreateMap<Comment, CommentEO>().ReverseMap();
        }
    }
}
