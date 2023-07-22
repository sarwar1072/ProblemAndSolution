using Autofac;
using Microsoft.AspNetCore.Mvc;
using ProblemAndSolution.Web.Models;
using System.Diagnostics;

namespace ProblemAndSolution.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ILifetimeScope _lifetimeScope;

        public HomeController(ILogger<HomeController> logger, ILifetimeScope lifetimeScope)
        {
            _logger = logger;
            _lifetimeScope = lifetimeScope;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> PaginatedQuestion(int index)
        {
            if (index > 0)
            {
                try
                {
                    var model = _lifetimeScope.Resolve<PublicLayoutModel>();
                    var questions = await model.GetQuestions(index);
                    return Ok(questions);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
            }
            return BadRequest();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}