using System;
using System.Collections.Generic;

namespace QuizBuilder.Models
{
    public partial class QuestionOption
    {
        public QuestionOption()
        {
            this.QuestionResponses = new List<QuestionRespons>();
        }

        public int QuestionOptionID { get; set; }
        public int QuestionID { get; set; }
        public string OptionSequence { get; set; }
        public string OptionText { get; set; }
        public bool IsRichText { get; set; }
        public bool IsCorrect { get; set; }
        public virtual Question Question { get; set; }
        public virtual ICollection<QuestionRespons> QuestionResponses { get; set; }
    }
}
