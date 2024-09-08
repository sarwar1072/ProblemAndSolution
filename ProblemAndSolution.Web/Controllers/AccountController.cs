using Autofac;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProblemAndSolution.Web.Enums;
using ProblemAndSolution.Web.Models;
using static System.Formats.Asn1.AsnWriter;

namespace ProblemAndSolution.Web.Controllers
{
    public class AccountController : BaseController<AccountController>
     { 
        IFileHelper _fileHelper;
        public AccountController(ILogger<AccountController> logger,ILifetimeScope lifetimeScope, IFileHelper fileHelper)
            : base(logger, lifetimeScope)
        {
            _fileHelper = fileHelper;   
        }
        [HttpGet]
        public async Task<IActionResult> AddProfile()
        {
            var model=_lifetimeScope.Resolve<UserProfileModel>();   
            return View(model); 
        }
        [Authorize(Roles = "User")]
        [HttpPost]
        public async Task<IActionResult> AddProfile(UserProfileModel model)
        {
            model.ResolveDependency(_lifetimeScope);
            if (ModelState.IsValid)
            {
                try
                {
                    model.ProfileURL = _fileHelper.UploadFile(model.formFile);
                    await model.AddProfile();
                    ViewResponse("Success", ResponseTypes.Success);
                    return RedirectToAction(nameof(AddProfile));
                }
                //catch (DuplicationException ex)
                //{
                //    ViewResponse("Duplicate", ResponseTypes.Warning);
                //   // return RedirectToAction(nameof(Add));
                //}
                catch (Exception ex)
                {
                    _logger.LogError($"{ex.Message}");
                    ViewResponse("Failure", ResponseTypes.Error);
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Register(string returnUrl=null!)
        {
            var model = _lifetimeScope.Resolve<RegisterModel>();
            model.ReturnUrl = returnUrl;
            await model.GetExternalAuthenticationSchemesAsync();
            return View(model);
        }
        [HttpPost,ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            model.Resolve(_lifetimeScope);
            model.ReturnUrl ??= Url.Content("~/");
            await model.GetExternalAuthenticationSchemesAsync();

            if (ModelState.IsValid)
            {
                try
                {
                    var result = await model.CreateAsync();
                    if (result.Succeeded)
                    {
                      // await model.AdduserProfile(model.UserId);
                        if (model.RequireConfirmedAccount())
                        {
                            return RedirectToAction("RegisterConfirmation", new { email = model.Email, returnUrl = model.ReturnUrl });
                        }
                        else
                        {
                            return RedirectToAction("Index", "Question", new { area = "ForPost" });
                        }
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
                catch (InvalidOperationException ex)
                {
                    _logger.LogError(ex, ex.Message);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);
                }
            }
            return View(model);
        }

        public async Task<IActionResult> RegisterConfirmation(string email,string returnUrl=null)
        {
            var model = _lifetimeScope.Resolve<RegistrationConfirmationModel>();
            var registerModel = _lifetimeScope.Resolve<RegisterModel>();

            if (email == null)
            {
                return RedirectToAction("Register");
            }

            try
            {
                var user = await registerModel.FindByEmailAsync(email);
                if (user == null)
                {
                    return NotFound($"Unable to load user with email '{email}'.");
                }

                model.Email = email;
               // await registerModel.EmailConfirmationTokenAsync();
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            var model = _lifetimeScope.Resolve<LoginModel>();
            var registerModel= _lifetimeScope.Resolve<RegisterModel>();

            if (!string.IsNullOrEmpty(model.ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, model.ErrorMessage);
            }

            returnUrl ??= Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await model.SignOutAsync();
            await registerModel.GetExternalAuthenticationSchemesAsync();
            model.ReturnUrl = returnUrl;
            return View(model);
        }
        [HttpPost,ValidateAntiForgeryToken]  
        public async Task<IActionResult> Login(LoginModel model)
        {
            model.Resolve(_lifetimeScope);
            var registerModel = _lifetimeScope.Resolve<RegisterModel>();
            model.ReturnUrl ??= Url.Content("~/");

            await registerModel.GetExternalAuthenticationSchemesAsync();

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await model.PasswordSignInAsync();
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    await model.RedirectByUserRole();
                    return LocalRedirect(model.ReturnUrl);
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout(string returnUrl = null!)
        {
            var model = _lifetimeScope.Resolve<LoginModel>();
            await model.SignOutAsync();
            _logger.LogInformation("User logged out.");
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return RedirectToAction();
            }
        }

    }
}
