using Autofac;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProblemAndSolution.Web.Controllers;

namespace ProblemAndSolution.Web.Areas.ForPost.Controllers
{
    [Area("ForPost")]
    [Authorize(Roles = "User")]
    public class PostBaseController<T> :BaseController<T> 
        where T :Controller
    {
        public PostBaseController(ILogger<T> logger,ILifetimeScope lifetimeScope):base(logger,lifetimeScope)
        {

        }
       
    }
}
