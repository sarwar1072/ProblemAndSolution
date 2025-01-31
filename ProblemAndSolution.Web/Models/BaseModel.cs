using Autofac;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using ProblemAndSolution.Membership.BusinessObj;
using ProblemAndSolution.Membership.DTOS;
using ProblemAndSolution.Membership.Services;
using System.CodeDom;

namespace ProblemAndSolution.Web.Models
{
    public abstract class BaseModel
    {
        protected IUserManagerAdapter<ApplicationUser> _userManagerAdapter;
        protected IHttpContextAccessor? _contextAccessor;
        protected IMapper? _mapper;
        protected ILifetimeScope? _lifetimeScope;
        public UserBasicInfo? basicInfo { get; private set; }
        public BaseModel()
        {
        }
        public BaseModel(IUserManagerAdapter<ApplicationUser> userManagerAdapter,IHttpContextAccessor contextAccessor,
            IMapper mapper)
        {
            _userManagerAdapter=userManagerAdapter; 
            _contextAccessor=contextAccessor;   
            _mapper=mapper; 
        }
       public virtual void ResolveDependency(ILifetimeScope lifetimeScope)
        {
            _lifetimeScope=lifetimeScope;
            _userManagerAdapter = _lifetimeScope.Resolve<IUserManagerAdapter<ApplicationUser>>();
            _contextAccessor = _lifetimeScope.Resolve<IHttpContextAccessor>();
            _mapper = _lifetimeScope.Resolve<IMapper>();
        }

        public ResponseModel? Response
        {
            get
            {
                if (_contextAccessor == null || _contextAccessor.HttpContext == null || !_contextAccessor.HttpContext.Session.IsAvailable)
                {
                    return null;  // Return null or handle the absence of HttpContext
                }

                if (_contextAccessor.HttpContext.Session.Keys.Contains(nameof(Response)))
                {
                    var response = _contextAccessor.HttpContext.Session.Get<ResponseModel>(nameof(Response));
                    _contextAccessor.HttpContext.Session.Remove(nameof(Response));
                    return response;
                }

                return null;
            }
            set
            {
                if (_contextAccessor != null && _contextAccessor.HttpContext != null)
                {
                    _contextAccessor.HttpContext.Session.Set(nameof(Response), value);
                }
            }
        }

        public  async virtual Task<bool> Userid(){

            var userName = _contextAccessor!.HttpContext!.User!.Identity!.Name;
            //var userInfo = await _userManagerAdapter!.FindByUsernameAsync(userName!);
            if(userName == null)
            {
                return false;
            }        
            return true;        
        }

        public async virtual Task GetUserInfoAsync()
        {
            var userName = _contextAccessor!.HttpContext!.User!.Identity!.Name;
            var userInfo = await _userManagerAdapter!.FindByUsernameAsync(userName!);
            basicInfo = new UserBasicInfo();
           _mapper!.Map(userInfo, basicInfo);                  
        } 

    }
}
