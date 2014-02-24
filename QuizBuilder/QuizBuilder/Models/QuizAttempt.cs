using System;
using System.Collections.Generic;

namespace QuizBuilder.Models
{
    public partial class QuizAttempt
    {
        public QuizAttempt()
        {
            this.QuestionResponses = new List<QuestionRespons>();
        }

        public int QuizAttemptID { get; set; }
        public int UserID { get; set; }
        public int QuizID { get; set; }
        public double Score { get; set; }
        public System.DateTime QuizDate { get; set; }
        public virtual ICollection<QuestionRespons> QuestionResponses { get; set; }
        public virtual Quizze Quizze { get; set; }
        public virtual User User { get; set; }
    }
}
