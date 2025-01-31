using Autofac;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProblemAndSolution.Web.Areas.ForPost.Models;
using ProblemAndSolution.Web.Enums;
using ProblemAndSolution.Web.Models;
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
                    model.Response=new ResponseModel("Question created successfully",ResponseType.Success);
                    //ViewResponse("Question created successfully", ResponseTypes.Success);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _logger.LogError("Failed to create");
                    model.Response = new ResponseModel("Question created failed", ResponseType.Failure);

                    //ViewResponse("Invalid credential",ResponseTypes.Warning);
                    return View(model);
                }               
            }
            catch(Exception ex){
                _logger.LogError(ex, "Failed to create Question");
                model.Response = new ResponseModel("Question created failed", ResponseType.Failure);

                // ViewResponse("Failed to create Question", ResponseTypes.Error);
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
                    model.Response = new ResponseModel("Question edited successfully", ResponseType.Success);

                   // ViewResponse("Question has been updated.", ResponseTypes.Success);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _logger.LogError("Failed to Update");
                    model.Response = new ResponseModel("Question failed ", ResponseType.Failure);

                    // ViewResponse("Invalid Creadentials.", ResponseTypes.Warning);
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to Update Question");
                model.Response = new ResponseModel("Question edit failed", ResponseType.Failure);

              //  ViewResponse("Failed to update Question", ResponseTypes.Error);
                return View(model);
            }
        }

        public async Task<IActionResult> DeleteQuestion(int id)
        {
            var model = _lifetimeScope.Resolve<QuestionEditModel>();

            try
            {
                await model.DeleteQuestionAsync(id);
                model.Response = new ResponseModel("Question deletion successfully", ResponseType.Success);

                //ViewResponse("Question has been successfully deleted.", ResponseTypes.Success);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to delete Question");
                model.Response = new ResponseModel("Question failed", ResponseType.Failure);

                //ViewResponse(ex.Message, ResponseTypes.Error);
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
