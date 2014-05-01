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
    public class QuizController : Controller
    {
        [Authorize(Roles = "admin")]
        public ActionResult ListQuiz()
        {
            var quizzes = QuizService.GetQuizzes();
            if (quizzes == null)
                quizzes = new Quiz[0];
            return PartialView(quizzes);
        }

        [Authorize(Roles = "admin")]
        public ActionResult CreateQuiz()
        {
            return PartialView();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateQuiz(Quiz model)
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
        public ActionResult ManageQuiz(int quizId)
        {
            var quiz = QuizService.FindQuiz(quizId);
            if (quiz != null)
                return View(quiz);
            else
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageQuiz(Quiz model)
        {
            if (ModelState.IsValid)
            {
                QuizService.UpdateQuiz(model);
            }
            return RedirectToAction("Home", "User");
        }

        public ActionResult NewQuiz(int quizId)
        {
            QuizAttempt newAttempt = new QuizAttempt { UserID = UserService.FindByName(User.Identity.Name).UserID, QuizID = quizId };
            newAttempt = QuizService.AddQuizAttempt(newAttempt);
            int[] quizScenarioIds = ScenarioService.GetScenarios().Where(s => s.QuizID == quizId).Select(s => s.ScenarioID).ToArray();
            ViewData.Add("QuestionArray", QuestionService.GetQuestions().Where(q => quizScenarioIds.Contains(q.ScenarioID)).ToArray());
            foreach(Question q in (Question[])ViewData["QuestionArray"])
            {
                ViewData.Add(q.QuestionID.ToString(), QuestionService.FindOptions(q.QuestionID));
            }
            ViewData.Add("ResponseArray", QuestionService.GetResponses(newAttempt.QuizAttemptID));
            return View();
        }

        public ActionResult ShowQuestion(int quizAttemptId, int questionId)
        {
            throw new NotImplementedException();
        }
	}
}