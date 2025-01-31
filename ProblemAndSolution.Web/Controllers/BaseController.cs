using Autofac;
using DevSkill.Http;
using Microsoft.AspNetCore.Mvc;
using ProblemAndSolution.Web.Enums;
using ProblemAndSolution.Web.Models;

namespace ProblemAndSolution.Web.Controllers
{
    public class BaseController<T> : Controller where T:Controller
    {
        protected readonly ILogger<T> _logger;
        protected readonly ILifetimeScope _lifetimeScope;
        public BaseController(ILogger<T> logger, ILifetimeScope lifetimeScope)
        {
            _logger = logger;
            _lifetimeScope = lifetimeScope;
        }

        //protected virtual void ViewResponse(string message, ResponseTypes responseTypes)
        //{
        //    TempData.Put("ResponseMessage", new ResponseModel
        //    {
        //        Message = message,
        //        Response = responseTypes,
        //    });
        //}
    }
}
