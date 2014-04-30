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
    }
}