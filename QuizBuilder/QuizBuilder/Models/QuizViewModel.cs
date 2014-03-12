using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuizBuilder.Models
{
    public class QuizViewModel
    {
    }
    public class LoginModel
    {
        public string Username { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
    public class SessionModel
    {
        public string Username { get; set; }
    }
}