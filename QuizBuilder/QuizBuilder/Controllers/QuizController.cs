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
        public ActionResult ListQuiz()
        {
            var quizzes = QuizService.GetQuizzes();
            if (quizzes == null)
                quizzes = new Quiz[0];
            return PartialView(quizzes);
        }

        public ActionResult ChooseQuiz()
        {
            var quizzes = QuizService.GetQuizzes();
            if (quizzes == null)
                quizzes = new Quiz[0];
            return View("ListQuiz", quizzes);
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
            QuizAttempt newAttempt = new QuizAttempt { UserID = UserService.FindByName(User.Identity.Name).UserID, QuizID = quizId, QuizDate = DateTime.Now };
            newAttempt = QuizService.AddQuizAttempt(newAttempt);
            int[] quizScenarioIds = ScenarioService.GetScenarios().Where(s => s.QuizID == quizId).Select(s => s.ScenarioID).ToArray();
            Question[] questionArray = QuestionService.GetQuestions().Where(q => quizScenarioIds.Contains(q.ScenarioID)).ToArray();
            ViewData.Add("QuestionArray", questionArray);
            ViewData.Add("SelectedQuestion", questionArray[0].QuestionID);
            return View("TakeQuiz", newAttempt);
        }

        public ActionResult TakeQuiz(int quizAttemptId, int? questionId)
        {
            QuizAttempt quizAttempt = QuizService.FindQuizAttempt(quizAttemptId);
            Quiz quiz = QuizService.FindQuiz(quizAttempt.QuizID);
            Scenario scenario = ScenarioService.FindScenario(quiz.QuizID);
            int[] quizScenarioIds = ScenarioService.GetScenarios().Where(s => s.QuizID == quiz.QuizID).Select(s => s.ScenarioID).ToArray();
            Question[] questionArray = QuestionService.GetQuestions().Where(q => quizScenarioIds.Contains(q.ScenarioID)).ToArray();
            ViewData.Add("QuestionArray", questionArray);
            if (questionId == null)
                ViewData.Add("SelectedQuestion", questionArray[0].QuestionID);
            else
                ViewData.Add("SelectedQuestion", (int)questionId);
            return View(quizAttempt);
        }

        public ActionResult ShowQuestion(int quizAttemptId, int questionId)
        {
            ViewData.Add("Question", QuestionService.FindQuestion(questionId));
            ViewData.Add("QuestionOptions", QuestionService.FindOptions(questionId));
            ViewData.Add("Attempt", QuizService.FindQuizAttempt(quizAttemptId));
            QuestionResponse[] existingResponse = QuestionService.GetResponses(quizAttemptId).Where(r => r.QuestionID == questionId).ToArray();
            if (existingResponse != null && existingResponse.Length == 1)
                ViewData.Add("ExistingResponse", existingResponse[0]);
            return PartialView();
        }

        public ActionResult PickQuestion(int quizAttemptId, int questionOptionId)
        {
            QuestionOption chosenOption = QuestionService.FindOption(questionOptionId);
            QuestionResponse[] existingResponse = QuestionService.GetResponses(quizAttemptId).Where(r => r.QuestionID == chosenOption.QuestionID).ToArray();
            if (existingResponse == null || existingResponse.Length == 0)
            {
                QuestionService.AddResponse(new QuestionResponse
                {
                    QuestionID = chosenOption.QuestionID,
                    QuestionOptionID = chosenOption.QuestionOptionID,
                    QuizAttemptID = quizAttemptId,
                    IsCorrect = chosenOption.IsCorrect,
                    UserReviewFlag = false
                });
                return RedirectToAction("TakeQuiz", new { quizAttemptId = quizAttemptId });
            }
            if (existingResponse != null && existingResponse.Length == 1)
            {
                existingResponse[0].QuestionOptionID = questionOptionId;
                QuestionService.UpdateResponse(existingResponse[0]);
                return RedirectToAction("TakeQuiz", new { quizAttemptId = quizAttemptId });
            }
            return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
        }

        public ActionResult ResultsQuiz()
        {
            QuizAttempt[] attempts = QuizService.GetUserAttempts(UserService.FindByName(User.Identity.Name).UserID);
            return View(attempts);
        }

        public ActionResult FinishQuiz(int quizAttemptId)
        {
            QuizAttempt attempt = QuizService.FindQuizAttempt(quizAttemptId);
            QuestionResponse[] responses = QuestionService.GetResponses(quizAttemptId);
            int score = (responses.Length / responses.Where(r => r.IsCorrect == true).ToArray().Length) * 100;
            attempt.Score = score;
            QuizService.UpdateQuizAttempt(attempt);
            return RedirectToAction("ResultsQuiz");
        }
    }
}