using System;
using System.Collections.Generic;

namespace QuizBuilder.Models
{
    public partial class Quizze
    {
        public Quizze()
        {
            this.QuizAttempts = new List<QuizAttempt>();
            this.Scenarios = new List<Scenario>();
        }

        public int QuizID { get; set; }
        public string QuizAuthor { get; set; }
        public string QuizName { get; set; }
        public int QuestionPoolID { get; set; }
        public virtual ICollection<QuizAttempt> QuizAttempts { get; set; }
        public virtual ICollection<Scenario> Scenarios { get; set; }
    }
}
