using Autofac;
using Microsoft.AspNetCore.Mvc;
using ProblemAndSolution.Web.Enums;
using ProblemAndSolution.Web.Models;
using System;
using ProblemAndSolution.Web.Areas.ForPost.Models.BlogModelFolder;
using static System.Formats.Asn1.AsnWriter;

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
            var model = _lifetimeScope.Resolve<BlogModel>();

            return View(model);
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
                   return RedirectToAction(nameof(Index));
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
        public async Task<IActionResult> Edit(int id)
        {
            var model=_lifetimeScope.Resolve<EditBlog>();
            await model.LoadData(id);
            return View(model); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditBlog model)
        {
            try
            {
                model.ResolveDependency(_lifetimeScope);
                if(ModelState.IsValid) {
                    if (model.formFile != null)
                    {
                        _fileHelper.DeleteFile(model.Url);
                        model.Url = _fileHelper.UploadFile(model.formFile);
                    }
                    await model.Update();
                    ViewResponse("Question has been updated", ResponseTypes.Success);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewResponse("Invalid", ResponseTypes.Warning);
                }
            }
            catch(Exception ex) { 
              ViewResponse("Failed to update",ResponseTypes.Error);
                return View(model);
            }
            return RedirectToAction(nameof(Index)); 
        }
        [HttpPost, ValidateAntiForgeryToken]

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var dataDelete = _lifetimeScope.Resolve<BlogModel>();
                 dataDelete.Delete(id);
                ViewResponse("Blog has been deleted", ResponseTypes.Success);
                return RedirectToAction(nameof(Index)); 
            }
            catch(Exception ex)
            {
                ViewResponse(ex.Message, ResponseTypes.Error);
            }
            return RedirectToAction(nameof(Index)); 

        }
        public IActionResult GetBlog()
        {
            var tableModel = new DataTablesAjaxRequestModel(Request);
            var model = _lifetimeScope.Resolve<BlogModel>();
            var data = model.GetBlog(tableModel);
            return Json(data);
        }


    }
}
