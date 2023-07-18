﻿using Autofac;
using AutoMapper;
using ProblemAndSolution.Membership.BusinessObj;
using ProblemAndSolution.Membership.DTOS;
using ProblemAndSolution.Membership.Services;
using System.CodeDom;

namespace ProblemAndSolution.Web.Models
{
    public abstract class BaseModel
    {
        protected IUserManagerAdapter<ApplicationUser>? _userManagerAdapter;
        protected IHttpContextAccessor? _contextAccessor;
        protected IMapper? _mapper;
        protected ILifetimeScope? _lifetimeScope;
        public UserBasicInfo? _basicInfo { get; private set; }
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
        public async virtual Task GetUserInfoAsync()
        {
            var userName = _contextAccessor!.HttpContext!.User!.Identity!.Name;
            var userInfo = await _userManagerAdapter!.FindByUsernameAsync(userName!);
           var  UserInfo = new UserBasicInfo();
            _mapper!.Map(userInfo, UserInfo);
        } 

    }
}
