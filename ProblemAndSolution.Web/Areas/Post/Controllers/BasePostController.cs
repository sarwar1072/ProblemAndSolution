using Autofac;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProblemAndSolution.Web.Controllers;

namespace ProblemAndSolution.Web.Areas.Post.Controllers
{
    [Area("Post")]
   // [Authorize(Roles = "User")]
    public class BasePostController<T> :BaseController<T> where T :Controller
    {
        public BasePostController(ILogger<T> logger,ILifetimeScope lifetimeScope):base(logger,lifetimeScope)
        {

        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
