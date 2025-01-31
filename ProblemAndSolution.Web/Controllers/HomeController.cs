using Autofac;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProblemAndSolution.Infrastructure.Services;
using ProblemAndSolution.Web.Models;
using System.Diagnostics;

namespace ProblemAndSolution.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ILifetimeScope _lifetimeScope;
        private IQuestionServices _qusetionService;
        private IAnswerServices _answerService;
        private IBlogServices _blogServices;
        public HomeController(ILogger<HomeController> logger, ILifetimeScope lifetimeScope,IBlogServices blogServices,
            IQuestionServices questionServices,IAnswerServices answerServices)
        {
            _blogServices = blogServices;   
            _answerService=answerServices;  
            _qusetionService=questionServices;  
            _logger = logger;
            _lifetimeScope = lifetimeScope;
        }
        public IActionResult Index()
        {
            var model = _lifetimeScope.Resolve<PublicLayoutModel>();
            ViewBag.Answer = _answerService.NumberOfAnswer();
            ViewBag.Question = _qusetionService.NumberOfQuestionAsync();
            ViewBag.Unasnwers = _qusetionService.NumberOfQuestionAsync() - _answerService.NumberOfAnswer();
            return View(model);
        }
        public async Task<IActionResult> PaginatedQuestion(int index)
        {
            var model = _lifetimeScope.Resolve<PublicLayoutModel>();
           // model.ResolveDependency(_lifetimeScope);

            if (index > 0)
            {
                try
                {
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
        public IActionResult BlogList()
        {
            try
            {

                var model = _lifetimeScope.Resolve<BlogViewModel>();
                // Assuming you have a mapping profile set up
                model.GetBlog();
              //  _logger.LogInformation("started successfully");

                //var blogs = new BlogViewModel()
                //{
                //    blogBOs = _blogServices.GetAllBlog()
                //};
                return View(model);
            }
            catch (Exception ex) {
                _logger.LogError(ex,"Failed to fetch blog details");
                ViewBag.Message = "Error";
            }
            return View();
        }
        public async Task<IActionResult> UserprofileDetails(Guid userId)
        {
            var model=_lifetimeScope.Resolve<UserProfileViewModel>();

            await model.GetUserProfile(userId);
           
            return View(model);  
        }
        public async Task<IActionResult> Details(int id)
        {
            var model = _lifetimeScope.Resolve<BlogViewModel>();
            model.ResolveDependency(_lifetimeScope);
            try
            {
              
                await model.GetDetailsById(id);

                await model.RecentPost();
                //await model.RelatedPost(id);    

                return View(model); 
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}");
                ViewBag.Message="Error";    
            }
            return View();  
        }
        [Authorize(Roles = "User,Admin")]
        [HttpPost]
        public async Task<IActionResult> Details(BlogViewModel model)
        {
            model.ResolveDependency(_lifetimeScope);

            try
            {
                //await model.GetUserInfoAsync();
                await model.AddComment();
                return RedirectToAction("Details", "Home");
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}");
                ViewBag.Message = "Error";
            }

            return View();
        }
        [Authorize(Roles="User,Admin")]
        [HttpPost]
        public async Task<IActionResult> AddLike(int quesId)
        {

            var model =_lifetimeScope.Resolve<BlogViewModel>();
            model.ResolveDependency(_lifetimeScope);
           // await model.GetUserInfoAsync();

            await model.AddLike(quesId);  
            return Ok(model);
        }
     
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}