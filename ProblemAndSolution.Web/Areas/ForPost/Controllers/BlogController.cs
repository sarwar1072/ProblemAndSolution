using Autofac;
using Microsoft.AspNetCore.Mvc;
using ProblemAndSolution.Web.Enums;
using ProblemAndSolution.Web.Models;
using System;
using ProblemAndSolution.Web.Areas.ForPost.Models.BlogModelFolder;
using static System.Formats.Asn1.AsnWriter;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace ProblemAndSolution.Web.Areas.ForPost.Controllers
{
    public class BlogController : PostBaseController<BlogController>
    {
        IFileHelper _fileHelper;

        public BlogController(IFileHelper fileHelper,ILogger<BlogController> logger, ILifetimeScope lifetimeScope) : base(logger, lifetimeScope)
        {
            _fileHelper=fileHelper; 
        }

        public async Task<IActionResult> Index()
        {
            var model = _lifetimeScope.Resolve<BlogModel>();

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            Guid user= Guid.Parse(claim.Value);
            await model.UserBlogList(user);
           
            //await model.PagingList(id, term, currentPage);
            
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
                    model.Response=new ResponseModel("Blog added successfully",ResponseType.Success);
                    //ViewResponse("Success", ResponseTypes.Success);
                   return RedirectToAction(nameof(Index));
                }
                //catch (DuplicationException ex)
                //{
                //    ViewResponse("Duplicate", ResponseTypes.Warning);
                //   // return RedirectToAction(nameof(Add));
                //}
                catch (Exception ex)
                {
                    _logger.LogError($"{ex.Message}");
                    model.Response = new ResponseModel("Creation failed",ResponseType.Failure);
                    //ViewResponse("Failure", ResponseTypes.Error);
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
                    model.Response = new ResponseModel("Blog edit successfully", ResponseType.Success);

                    //ViewResponse("Question has been updated", ResponseTypes.Success);
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _logger.LogError("Failed to edit");
                    model.Response = new ResponseModel("Blog added failed", ResponseType.Failure);

                    // ViewResponse("Invalid", ResponseTypes.Warning);
                }
            }
            catch(Exception ex) {
                _logger.LogError($"{ex.Message}");
                model.Response = new ResponseModel("Blog edit failed", ResponseType.Failure);

                //ViewResponse("Failed to update",ResponseTypes.Error);
                return View(model);
            }
            return RedirectToAction(nameof(Index)); 
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var dataDelete = _lifetimeScope.Resolve<BlogModel>();

            try
            {
                 dataDelete.Delete(id);
               dataDelete.Response = new ResponseModel("Deleted successfully",ResponseType.Success);
                //ViewResponse("Blog has been deleted", ResponseTypes.Success);
                return RedirectToAction(nameof(Index)); 
            }
            catch(Exception ex)
            {
                _logger.LogError($"{ex.Message}");
                dataDelete.Response = new ResponseModel("Deletion failed",ResponseType.Failure);
               // ViewResponse(ex.Message, ResponseTypes.Error);
            }
            return RedirectToAction(nameof(Index)); 
        }
        public async Task<IActionResult> ApprovePost(int id)
        {
            var model = _lifetimeScope.Resolve<EditBlog>();

            try
            {
                await model.ApproveSinglePost(id);
                model.Response = new ResponseModel("Approved", ResponseType.Success);    
               // ViewResponse("Blog post approved", ResponseTypes.Success);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                _logger.LogError($"{ex.Message}");
                model.Response= new ResponseModel("error", ResponseType.Failure);   
                //ViewResponse(ex.Message, ResponseTypes.Error);
            }
            return RedirectToAction(nameof(Index));           
        }
        public async Task<IActionResult> Approve()
        {
            var model= _lifetimeScope.Resolve<BlogModel>();
           await  model.ListOfBlogs();

            return View(model);
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
