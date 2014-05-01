using QuizBuilder.Models;
using QuizBuilder.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace QuizBuilder.Controllers
{
    [Authorize]
    public class QuestionController : Controller
    {
        [Authorize(Roles = "admin")]
        public ActionResult ListQuestion()
        {
            var questions = QuestionService.GetQuestions();
            if (questions == null)
                questions = new Question[0];
            return PartialView(questions);
        }

        [Authorize(Roles = "admin")]
        public ActionResult CreateQuestion()
        {
            SelectList scenarioList = new SelectList(ScenarioService.GetScenarios(), "ScenarioID", "ScenarioName");
            ViewData.Add("ScenarioID", scenarioList);
            return PartialView();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateQuestion(Question model)
        {
            if (ModelState.IsValid)
            {
                QuestionService.AddQuestion(model);
                return RedirectToAction("Home", "User");
            }
            else
            {
                return RedirectToAction("Home", "User");
            }
        }

        [Authorize(Roles = "admin")]
        public ActionResult ManageQuestion(int questionId)
        {
            var scenario = QuestionService.FindQuestion(questionId);
            if (scenario != null)
            {
                QuestionOption[] optionList = QuestionService.FindOptions(questionId);
                SelectList scenarioList = new SelectList(ScenarioService.GetScenarios(), "ScenarioID", "ScenarioName");
                foreach (SelectListItem q in scenarioList)
                {
                    if (q.Value == scenario.ScenarioID.ToString())
                        q.Selected = true;
                }
                ViewData.Add("ScenarioID", scenarioList);
                ViewData.Add("Options", optionList);
                return View(scenario);
            }
            else
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageQuestion(Question model)
        {
            if (ModelState.IsValid)
            {
                QuestionService.UpdateQuestion(model);
            }
            return RedirectToAction("Home", "User");
        }

        [Authorize(Roles = "admin")]
        public ActionResult ListOption(int questionId)
        {
            var options = QuestionService.FindOptions(questionId);
            if (options == null)
                options = new QuestionOption[0];
            return PartialView(options);
        }

        [Authorize(Roles = "admin")]
        public ActionResult CreateOption(int questionId)
        {
            ViewData.Add("QuestionID", questionId);
            return PartialView();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateOption(QuestionOption model)
        {
            if (ModelState.IsValid)
            {
                QuestionService.AddOption(model);
                return RedirectToAction("Home", "User");
            }
            else
            {
                return RedirectToAction("Home", "User");
            }
        }

        [Authorize(Roles = "admin")]
        public ActionResult ManageOption(int questionId, int optionId)
        {
            var option = QuestionService.FindOption(optionId);
            if (option != null)
            {
                SelectList scenarioList = new SelectList(QuestionService.GetQuestions(), "QuestionID", "QuestionID");
                foreach (SelectListItem q in scenarioList)
                {
                    if (q.Value == option.QuestionID.ToString())
                        q.Selected = true;
                }
                ViewData.Add("QuestionID", questionId);
                return PartialView(option);
            }
            else
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageOption(QuestionOption model)
        {
            if (ModelState.IsValid)
            {
                QuestionService.UpdateOption(model);
            }
            return RedirectToAction("Home", "User");
        }
	}
}