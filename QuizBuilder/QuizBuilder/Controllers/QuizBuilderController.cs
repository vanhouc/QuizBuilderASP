using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuizBuilder.Models;
using QuizBuilder.Services;
using System.Net;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using QuizBuilder.DataContexts;

namespace QuizBuilder.Controllers
{
    [Authorize]
    public class QuizBuilderController : Controller
    {
        public QuizBuilderController()
            : this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new IdentityDb())))
        {
        }

        public QuizBuilderController(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
        }

        public UserManager<ApplicationUser> UserManager { get; private set; }

        [AllowAnonymous]
        //
        // GET: /QuizBuilder/
        public ActionResult Default()
        {
            if (HttpContext.User.IsInRole("admin"))
                return View("AdminHome");
            if (HttpContext.User.IsInRole("user"))
                return View("UserHome");
            return RedirectToAction("Login", "Account");
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
        public ActionResult AdminUsers()
        {
            return PartialView(UserService.GetUsers());
        }
        public ActionResult UserDetails(int? id)
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
                        return PartialView("UserDetails", UserService.CreateUserModel(userExtData));
                    else
                        throw new ApplicationException("Could not find newly created data");
                }
                else
                {
                    return PartialView("UserDetails", UserService.CreateUserModel(userExtData));
                }
            }
            else
            {
                User userExtData = UserService.FindUser((int)id);
                if (userExtData != null)
                    return PartialView("UserDetails", UserService.CreateUserModel(userExtData));
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

        [Authorize(Roles = "admin")]
        public ActionResult ListQuizzes()
        {
            var quizzes = QuizService.GetQuizzes();
            if (quizzes == null)
                quizzes = new Quiz[0];
            return PartialView("QuizList", quizzes);
        }

        [Authorize(Roles = "admin")]
        public ActionResult AddQuiz()
        {
            return PartialView("CreateQuiz");
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddQuiz(Quiz model)
        {
            if (ModelState.IsValid)
            {
                QuizService.AddQuiz(model);
                return RedirectToAction("Default");
            }
            else
            {
                return RedirectToAction("Default");
            }
        }

        [Authorize(Roles = "admin")]
        public ActionResult ListScenarios()
        {
            var scenarios = ScenarioService.GetScenarios();
            if (scenarios == null)
                scenarios = new Scenario[0];
            return PartialView("ScenarioList", scenarios);
        }

        [Authorize(Roles = "admin")]
        public ActionResult AddScenario()
        {
            SelectList quizList = new SelectList(QuizService.GetQuizzes(), "QuizId", "QuizName");
            ViewData.Add("QuizID", quizList);
            return PartialView("CreateScenario");
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddScenario(Scenario model)
        {
            if (ModelState.IsValid)
            {
                ScenarioService.AddScenario(model);
                return RedirectToAction("Default");
            }
            else
            {
                return RedirectToAction("Default");
            }
        }

        public Scenario[] GetScenarios()
        {
            return (ScenarioService.GetScenarios()) ?? new Scenario[0];
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Default", "QuizBuilder");
            }
        }
    }
}