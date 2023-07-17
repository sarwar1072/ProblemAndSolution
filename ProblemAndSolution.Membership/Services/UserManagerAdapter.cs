using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Query.Internal;
using ProblemAndSolution.Membership.BusinessObj;
using ProblemAndSolution.Membership.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationUserEO = ProblemAndSolution.Infrastructure.Entities.Membership.ApplicationUser;

namespace ProblemAndSolution.Membership.Services
{
    public class UserManagerAdapter:IUserManagerAdapter<ApplicationUser>
    {
        private readonly UserManager _userManager;
        private readonly SignInManager _signInManager;
        private  IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        public UserManagerAdapter(UserManager userManager,SignInManager signInManager,
            IMapper mapper,IHttpContextAccessor contextAccessor)
        {
            _userManager=userManager;
            _signInManager=signInManager;   
            _mapper=mapper;
            _contextAccessor=contextAccessor;   
        }
        private ApplicationUserEO GetSingleEntity(ApplicationUser user)
        {
            var entity = _mapper.Map<ApplicationUserEO>(user);
            return entity;  
        }

        private ApplicationUser GetSingleBusinessObj(ApplicationUserEO userEO)
        {
            var user=_mapper.Map<ApplicationUser>(userEO);
            return user;
        }
        public async Task<IdentityResult> CreateAsync(ApplicationUser user,string password)
        {
            if (user == null)
                throw new InvalidOperationException("User must be provided to created member");

            if(string.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException("password"); 
            
             var entity=GetSingleEntity(user);
            var result = await _userManager.CreateAsync(entity, password);

            if (!result.Succeeded)
            {
                return result;
            }
            var roleResult = await _userManager.AddToRoleAsync(entity,"User");

            await SignInAsync(entity);

            if (!roleResult.Succeeded)
            {
                return roleResult;
            }

            return result; 
        }

        private async Task SignInAsync(ApplicationUserEO user)
        {
            await _signInManager.SignInAsync(user, isPersistent: false);
        }

    }
}
