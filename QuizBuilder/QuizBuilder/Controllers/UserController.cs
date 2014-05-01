using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using QuizBuilder.DataContexts;
using QuizBuilder.Models;
using QuizBuilder.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace QuizBuilder.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        public UserController()
            : this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new IdentityDb())))
        {
        }

        public UserController(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
        }

        public UserManager<ApplicationUser> UserManager { get; private set; }
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }
        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindAsync(model.UserName, model.Password);
                if (user != null)
                {
                    await SignInAsync(user, model.RememberMe);
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser() { UserName = model.UserName };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    UserService.AddUser(new User
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Username = model.UserName,
                        Password = UserManager.PasswordHasher.HashPassword(model.Password),
                        Email = model.EmailAddress,
                        IsAdmin = false
                    });
                    await SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Home", "User");
                }
                else
                {
                    AddErrors(result);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        [AllowAnonymous]
        //
        // GET: /QuizBuilder/
        public ActionResult Home()
        {
            if (HttpContext.User.IsInRole("admin"))
                return View("HomeAdmin");
            if (HttpContext.User.IsInRole("user"))
                return View("HomeUser");
            return RedirectToAction("Login");
        }
        //
        // GET: /QuizBuilder/UserHome/
        public ActionResult UserQuizzes()
        {
            return View();
        }
        public ActionResult UserResults()
        {
            return View();
        }
        public ActionResult ListUser()
        {
            return PartialView(UserService.GetUsers());
        }
        public ActionResult ManageUser(int? id)
        {
            if (id == null)
            {
                ApplicationUser user = UserManager.FindById(User.Identity.GetUserId());
                if (user == null)
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                User userExtData = UserService.FindByName(user.UserName);
                if (userExtData == null)
                {
                    UserService.AddUser(new User
                    {
                        FirstName = "Empty",
                        LastName = "Empty",
                        Username = user.UserName,
                        Password = user.PasswordHash,
                        Email = "fake@fake.com",
                        IsAdmin = (UserManager.IsInRole(user.Id, "admin") ? true : false)
                    });
                    userExtData = UserService.FindByName(user.UserName);
                    if (userExtData != null)
                        return PartialView("ManageUser", UserService.CreateUserModel(userExtData));
                    else
                        throw new ApplicationException("Could not find newly created data");
                }
                else
                {
                    return PartialView("ManageUser", UserService.CreateUserModel(userExtData));
                }
            }
            else
            {
                User userExtData = UserService.FindUser((int)id);
                if (userExtData != null)
                    return PartialView("ManageUser", UserService.CreateUserModel(userExtData));
                else
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateUser(QuizUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                UserService.UpdateUser(model);
                return RedirectToAction("Default");
            }
            else
            {
                return View("Default", model);
            }
        }
        // GET: /QuizAuto/Delete/5
        public ActionResult DeleteUser(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = UserService.FindUser(id.Value);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: /QuizAuto/Delete/5
        [HttpPost, ActionName("DeleteUser")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserService.DeleteUser(id);
            return RedirectToAction("AdminHome");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "User");
        }

        #region Utilities

        private const string XsrfKey = "XsrfId";
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            Error
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "QuizBuilder");
            }
        }

        private class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties() { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}