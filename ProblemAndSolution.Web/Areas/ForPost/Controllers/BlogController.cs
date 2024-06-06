using Autofac;
using Microsoft.AspNetCore.Mvc;
using ProblemAndSolution.Web.Enums;
using ProblemAndSolution.Web.Models;
using System;
using ProblemAndSolution.Web.Areas.ForPost.Models.BlogModel;

namespace ProblemAndSolution.Web.Areas.ForPost.Controllers
{
    public class BlogController : PostBaseController<BlogController>
    {
        IFileHelper _fileHelper;

        public BlogController(IFileHelper fileHelper,ILogger<BlogController> logger, ILifetimeScope lifetimeScope) : base(logger, lifetimeScope)
        {
            _fileHelper=fileHelper; 
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = _lifetimeScope.Resolve<CreateBlog>();
            return View(model);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(CreateBlog model)
        {

            model.ResolveDependency(_lifetimeScope);
            if (ModelState.IsValid)
            {
                try
                {
                    model.Url = _fileHelper.UploadFile(model.formFile);
                   await model.AddBlog();
                    ViewResponse("Success", ResponseTypes.Success);
                  //  return RedirectToAction(nameof(Add));
                }
                //catch (DuplicationException ex)
                //{
                //    ViewResponse("Duplicate", ResponseTypes.Warning);
                //   // return RedirectToAction(nameof(Add));
                //}
                catch (Exception ex)
                {
                    ViewResponse("Failure", ResponseTypes.Error);
                }
            }
            return View(model);
        }

    }
}
