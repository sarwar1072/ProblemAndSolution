using AutoMapper;
using ProblemAndSolution.Infrastructure.BusinessObj.Membership;
using ProblemAndSolution.Membership.DTOS;
using ProblemAndSolution.Web.Models;

namespace ProblemAndSolution.Web.Profiles
{
    public class WebProfile : Profile
    {
        public WebProfile()
        {
            CreateMap<RegisterModel, ApplicationUser>().ReverseMap();
            CreateMap<ApplicationUser, UserBasicInfo>().ReverseMap();
            //CreateMap<Question, QuestionCreateModel>().ReverseMap();
            //CreateMap<Question, QuestionEditModel>().ReverseMap();

        }
    }
}
