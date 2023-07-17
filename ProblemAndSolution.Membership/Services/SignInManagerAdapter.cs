using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using ProblemAndSolution.Membership.BusinessObj;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationUserEO = ProblemAndSolution.Infrastructure.Entities.Membership.ApplicationUser;

namespace ProblemAndSolution.Membership.Services
{
    public class SignInManagerAdapter: ISignInManagerAdapter<ApplicationUser>
    {
        private readonly SignInManager _signInManager;
        private IMapper _mapper;
        public SignInManagerAdapter(SignInManager signInManager, IMapper mapper)
        {
            _signInManager = signInManager;
            _mapper = mapper;   
        }
        private ApplicationUserEO GetSingleEntity(ApplicationUser applicationUser)
        {
            var user = _mapper.Map<ApplicationUserEO>(applicationUser);
            return user;
        }
        public async Task<IEnumerable<AuthenticationScheme>> GetExternalSchemeAsync()
        {
            return await _signInManager.GetExternalAuthenticationSchemesAsync();
        }

    }
}
