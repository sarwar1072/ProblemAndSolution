using Autofac;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProblemAndSolution.Web.Areas.ForPost.Models;
using ProblemAndSolution.Web.Enums;
using System.Linq.Expressions;
using static System.Formats.Asn1.AsnWriter;

namespace ProblemAndSolution.Web.Areas.ForPost.Controllers
{
    public class QuestionController : PostBaseController<QuestionController>
    {
        public QuestionController(ILogger<QuestionController> logger,ILifetimeScope lifetimeScope):base(
            logger, lifetimeScope)  
        {

        }

        public async Task<IActionResult> Index()
        {
            var model = _lifetimeScope.Resolve<QuestionEditModel>();
            await model.GetUserSpecificPost();
            return View(model);
        }
        [HttpGet]  
        public async Task<IActionResult> Create()
        {
            var model = _lifetimeScope.Resolve<QuestionCreateModel>();
            await model.GetUserInfoAsync();
            return View(model);
        }
        [HttpPost,ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(QuestionCreateModel model)
        {
            try
            {
                model.ResolveDependency(_lifetimeScope);
                await model.GetUserInfoAsync();

                if (ModelState.IsValid)
                {
                    await model.AddQuestionAsync();
                    ViewResponse("Question created successfully", ResponseTypes.Success);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewResponse("Invalid credential",ResponseTypes.Warning);
                    return View(model);
                }               
            }
            catch(Exception ex){
                _logger.LogError(ex, "Failed to create Question");
                ViewResponse("Failed to create Question", ResponseTypes.Error);
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = _lifetimeScope.Resolve<QuestionEditModel>();
            await model.GetUserInfoAsync();
            await model.GetByIdAsync(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(QuestionEditModel model)
        {

            try
            {
                model.ResolveDependency(_lifetimeScope);
                await model.GetUserInfoAsync();
                if (ModelState.IsValid)
                {

                    await model.UpdateAsync();
                    ViewResponse("Question has been updated.", ResponseTypes.Success);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewResponse("Invalid Creadentials.", ResponseTypes.Warning);
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to Update Question");
                ViewResponse("Failed to update Question", ResponseTypes.Error);
                return View(model);
            }
        }

        public async Task<IActionResult> DeleteQuestion(int id)
        {
            try
            {
                var model = _lifetimeScope.Resolve<QuestionEditModel>();
                await model.DeleteQuestionAsync(id);
                ViewResponse("Question has been successfully deleted.", ResponseTypes.Success);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to delete Question");      
                ViewResponse(ex.Message, ResponseTypes.Error);
            }
            return RedirectToAction(nameof(Index));
        }

        [AllowAnonymous]
        public IActionResult GetPosts()
        {
            var model = _lifetimeScope.Resolve<QuestionEditModel>();
            model.GetTemp();
            return View();
        }


        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var model = _lifetimeScope.Resolve<QuestionEditModel>();
            await model.Details(id);
            return View(model);
        }


    }
}
