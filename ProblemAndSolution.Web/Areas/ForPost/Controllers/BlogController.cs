using Autofac;
using Microsoft.AspNetCore.Mvc;

namespace ProblemAndSolution.Web.Areas.ForPost.Controllers
{
    public class BlogController : PostBaseController<BlogController>
    {
        public BlogController(ILogger<BlogController> logger, ILifetimeScope lifetimeScope) : base(logger, lifetimeScope)
        {

        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
