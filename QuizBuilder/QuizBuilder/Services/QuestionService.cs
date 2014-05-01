using QuizBuilder.DataContexts;
using QuizBuilder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuizBuilder.Services
{
    public static class QuestionService
    {
        public static Question AddQuestion(Question question)
        {
            using (QuizBuilderDb db = new QuizBuilderDb())
            {
                Question toAdd = question;
                db.Questions.Add(toAdd);
                db.SaveChanges();
                return toAdd;
            }
        }

        public static Question[] GetQuestions()
        {
            using (QuizBuilderDb db = new QuizBuilderDb())
            {
                return (from q in db.Questions
                        select q).ToArray<Question>();
            }
        }

        public static Question FindQuestion(int questionId)
        {
            using (QuizBuilderDb db = new QuizBuilderDb())
            {
                return db.Questions.Find(questionId);
            }
        }

        public static QuestionOption AddOption(QuestionOption option)
        {
            using (QuizBuilderDb db = new QuizBuilderDb())
            {
                QuestionOption toAdd = option;
                db.QuestionOptions.Add(toAdd);
                db.SaveChanges();
                return toAdd;
            }
        }

        public static QuestionOption FindOption(int optionId)
        {
            using (QuizBuilderDb db = new QuizBuilderDb())
            {
                return db.QuestionOptions.Find(optionId);
            }
        }

        public static QuestionOption[] FindOptions(int questionId)
        {
            using (QuizBuilderDb db = new QuizBuilderDb())
            {
                return (from q in db.QuestionOptions
                        where q.QuestionID == questionId
                        select q).ToArray<QuestionOption>();
            }
        }

        public static Question UpdateQuestion(Question updatedQuestion)
        {
            using (QuizBuilderDb db = new QuizBuilderDb())
            {
                Question currentQuestion = db.Questions.Find(updatedQuestion.QuestionID);
                if (currentQuestion != null)
                {
                    currentQuestion.QuestionContent = updatedQuestion.QuestionContent;
                    currentQuestion.QuestionTypeID = updatedQuestion.QuestionTypeID;
                    currentQuestion.AnswerContent = updatedQuestion.AnswerContent;
                    db.SaveChanges();
                    return currentQuestion;
                }
                else
                    return currentQuestion;
            }
        }
        public static QuestionOption UpdateOption(QuestionOption updatedOption)
        {
            using (QuizBuilderDb db = new QuizBuilderDb())
            {
                QuestionOption currentOption = db.QuestionOptions.Find(updatedOption.QuestionID);
                if (currentOption != null)
                {
                    currentOption.OptionText = updatedOption.OptionText;
                    currentOption.IsRichText = updatedOption.IsRichText;
                    currentOption.IsCorrect = updatedOption.IsCorrect;
                    currentOption.OptionSequence = updatedOption.OptionSequence;
                    db.SaveChanges();
                    return currentOption;
                }
                else
                    return currentOption;
            }
        }
        public static QuestionResponse AddResponse(QuestionResponse response)
        {
            using (QuizBuilderDb db = new QuizBuilderDb())
            {
                QuestionResponse toAdd = response;
                db.QuestionResponses.Add(toAdd);
                db.SaveChanges();
                return toAdd;
            }
        }
        public static QuestionResponse UpdateResponse(QuestionResponse updatedResponse)
        {
            using (QuizBuilderDb db = new QuizBuilderDb())
            {
                QuestionResponse currentReponse = db.QuestionResponses.Find(updatedResponse.QuestionResponseID);
                if (currentReponse != null)
                {
                    currentReponse.QuestionOptionID = updatedResponse.QuestionOptionID;
                    currentReponse.QuestionID = updatedResponse.QuestionID;
                    currentReponse.IsCorrect = updatedResponse.IsCorrect;
                    currentReponse.UserReviewFlag = updatedResponse.UserReviewFlag;
                    currentReponse.QuizAttemptID = updatedResponse.QuizAttemptID;
                    db.SaveChanges();
                    return currentReponse;
                }
                else
                {
                    return currentReponse;
                }
            }
        }
        public static QuestionResponse[] GetResponses(int attemptId)
        {
            using (QuizBuilderDb db = new QuizBuilderDb())
            {
                return db.QuestionResponses.Where(r => r.QuizAttemptID == attemptId).ToArray();
            }
        }
    }
}