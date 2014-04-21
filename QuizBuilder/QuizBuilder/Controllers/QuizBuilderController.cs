using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuizBuilder.Models;
using QuizBuilder.Services;
using System.Net;
using System.Data.Entity;

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
        public ActionResult UserHome(User user)
        {
            if (user != null)
            {
                if (UserService.FindUser(user.UserID).IsAdmin == true)
                    return View("AdminHome", user);
                if (UserService.FindUser(user.UserID).IsAdmin == false)
                {
                    return View("UserHome", user);
                }
                else
                {
                    return RedirectToAction("UserLogin");
                }
            }
            else
                return RedirectToAction("UserLogin");
        }
        public ActionResult Login()
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
            return PartialView(UserService.GetUsers());
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
                User newUser = UserService.AddUser(model);
                if (newUser != null)
                    return View("UserHome", newUser);
                else
                {
                    ModelState.AddModelError("", "Invalid Submission");
                }
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User user = UserService.ValidateLogin(model);
                if (user != null)
                    return UserHome(user);
            }
            return RedirectToAction("Login");
        }
        public ActionResult UserDetails(int? id)
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
            return PartialView("UserDetails", user);
        }

        // GET: /QuizAuto/Edit/5
        public ActionResult EditUser(int? id)
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

        // POST: /QuizAuto/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUser([Bind(Include = "UserID,FirstName,LastName,Username,Password,Email,IsAdmin")] User user)
        {
            if (ModelState.IsValid)
            {
                UserService.SaveChanges(user);
                return RedirectToAction("AdminUsers");
            }
            return View(user);
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
    }
}