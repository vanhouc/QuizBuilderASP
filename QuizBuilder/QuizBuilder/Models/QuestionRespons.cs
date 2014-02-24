using System;
using System.Collections.Generic;

namespace QuizBuilder.Models
{
    public partial class QuestionRespons
    {
        public int QuestionResponseID { get; set; }
        public int QuestionOptionID { get; set; }
        public int QuizAttemptID { get; set; }
        public int QuestionID { get; set; }
        public bool IsCorrect { get; set; }
        public bool UserReviewFlag { get; set; }
        public virtual QuestionOption QuestionOption { get; set; }
        public virtual Question Question { get; set; }
        public virtual QuizAttempt QuizAttempt { get; set; }
    }
}
