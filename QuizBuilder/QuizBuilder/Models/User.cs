using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
        public virtual ICollection<QuizAttempt> QuizAttempts { get; set; }
    }
}
