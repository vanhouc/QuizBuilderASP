using System;
using System.Collections.Generic;

namespace QuizBuilder.Models
{
    public partial class Scenario
    {
        public Scenario()
        {
            this.Questions = new List<Question>();
        }

        public int ScenarioID { get; set; }
        public int QuizID { get; set; }
        public int ScenarioSequence { get; set; }
        public string ScenarioName { get; set; }
        public string ScenarioText { get; set; }
        public bool IsRichText { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
        public virtual Quiz Quizze { get; set; }
    }
}
