using AutoMapper;
using ApplicationUserEO=ProblemAndSolution.Infrastructure.Entities.Membership.ApplicationUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProblemAndSolution.Membership.BusinessObj;

namespace ProblemAndSolution.Membership.Profiles
{
    public class MembershipProfile:Profile
    {
        public MembershipProfile()
        {
            CreateMap<ApplicationUser, ApplicationUserEO>().ReverseMap();

        }
    }
}
