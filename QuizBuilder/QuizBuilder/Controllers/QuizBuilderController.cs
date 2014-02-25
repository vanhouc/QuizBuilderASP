using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuizBuilder.Models;
using QuizBuilder.Services;

namespace QuizBuilder.Controllers
{
    public class QuizBuilderController : Controller
    {
        //
        // GET: /QuizBuilder/
        public ActionResult Default()
        {
            return View();
        }
        //
        // GET: /QuizBuilder/UserHome/
        public ActionResult UserHome()
        {
            return View();
        }
        public ActionResult UserQuizzes()
        {
            return View();
        }
        public ActionResult UserResults()
        {
            return View();
        }
        public ActionResult AdminHome()
        {
            return View();
        }
        public ActionResult AdminUsers()
        {
            UserService userService = new UserService();
            return View(userService.getUsers());
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User model)
        {
            if (ModelState.IsValid)
            {
                UserService userService = new UserService();
                if (userService.createUser(model))
                    return View("UserHome");
                else
                {
                    ModelState.AddModelError("", "Invalid Submission");
                }
            }
            return View(model);
        }
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}