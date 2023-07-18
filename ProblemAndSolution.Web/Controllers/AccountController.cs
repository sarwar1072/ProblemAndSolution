using Microsoft.AspNetCore.Mvc;

namespace ProblemAndSolution.Web.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
