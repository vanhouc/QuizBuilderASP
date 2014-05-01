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
    public class ScenarioController : Controller
    {
        [Authorize(Roles = "admin")]
        public ActionResult ListScenario()
        {
            var scenarios = ScenarioService.GetScenarios();
            if (scenarios == null)
                scenarios = new Scenario[0];
            return PartialView(scenarios);
        }

        [Authorize(Roles = "admin")]
        public ActionResult CreateScenario()
        {
            SelectList quizList = new SelectList(QuizService.GetQuizzes(), "QuizID", "QuizName");
            ViewData.Add("QuizID", quizList);
            return PartialView();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateScenario(Scenario model)
        {
            if (ModelState.IsValid)
            {
                ScenarioService.AddScenario(model);
                return RedirectToAction("Home", "User");
            }
            else
            {
                return RedirectToAction("Home", "User");
            }
        }
        [Authorize(Roles = "admin")]
        public ActionResult ManageScenario(int scenarioId)
        {
            var scenario = ScenarioService.FindScenario(scenarioId);
            if (scenario != null)
            {
                SelectList quizList = new SelectList(QuizService.GetQuizzes(), "QuizID", "QuizName");
                foreach (SelectListItem q in quizList)
                {
                    if (q.Value == scenario.QuizID.ToString())
                        q.Selected = true;
                }
                ViewData.Add("QuizID", quizList);
                return View(scenario);
            }
            else
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageScenario(Scenario model)
        {
            if (ModelState.IsValid)
            {
                ScenarioService.UpdateScenario(model);
            }
            return RedirectToAction("Home", "User");
        }
    }
}