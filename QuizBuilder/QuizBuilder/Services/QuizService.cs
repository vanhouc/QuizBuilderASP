using QuizBuilder.DataContexts;
using QuizBuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizBuilder.Services
{
    public static class QuizService
    {
        public static Quiz AddQuiz(Quiz quiz)
        {
            using (QuizBuilderDb db = new QuizBuilderDb())
            {
                Quiz toAdd = quiz;
                db.Quizzes.Add(toAdd);
                db.SaveChanges();
                return toAdd;
            }
        }

        public static Quiz[] GetQuizzes()
        {
            using (QuizBuilderDb db = new QuizBuilderDb())
            {
                return (from q in db.Quizzes
                        select q).ToArray<Quiz>();
            }
        }

        public static Quiz FindQuiz(int quizId)
        {
            using (QuizBuilderDb db = new QuizBuilderDb())
            {
                return db.Quizzes.Find(quizId);
            }
        }

        public static Quiz UpdateQuiz(Quiz updatedQuiz)
        {
            using (QuizBuilderDb db = new QuizBuilderDb())
            {
                Quiz currentQuiz = db.Quizzes.Find(updatedQuiz.QuizID);
                if (currentQuiz != null)
                {
                    currentQuiz.QuizName = updatedQuiz.QuizName;
                    currentQuiz.QuizName = updatedQuiz.QuizAuthor;
                    currentQuiz.QuestionPoolID = updatedQuiz.QuestionPoolID;
                    db.SaveChanges();
                    return currentQuiz;
                }
                else
                    return currentQuiz;
            }
        }

        public static QuizAttempt AddQuizAttempt(QuizAttempt quizAttempt)
        {
            using (QuizBuilderDb db = new QuizBuilderDb())
            {
                QuizAttempt toAdd = quizAttempt;
                db.QuizAttempts.Add(toAdd);
                db.SaveChanges();
                return toAdd;
            }
        }
    }
}