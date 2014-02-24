using System;
using System.Collections.Generic;

namespace QuizBuilder.Models
{
    public partial class Question
    {
        public Question()
        {
            this.QuestionOptions = new List<QuestionOption>();
            this.QuestionResponses = new List<QuestionRespons>();
        }

        public int QuestionID { get; set; }
        public int ScenarioID { get; set; }
        public int QuestionTypeID { get; set; }
        public string QuestionContent { get; set; }
        public string AnswerContent { get; set; }
        public virtual ICollection<QuestionOption> QuestionOptions { get; set; }
        public virtual ICollection<QuestionRespons> QuestionResponses { get; set; }
        public virtual Scenario Scenario { get; set; }
    }
}
