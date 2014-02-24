using System;
using System.Collections.Generic;

namespace QuizBuilder.Models
{
    public partial class User
    {
        public User()
        {
            this.QuizAttempts = new List<QuizAttempt>();
        }

        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
        public virtual ICollection<QuizAttempt> QuizAttempts { get; set; }
    }
}
