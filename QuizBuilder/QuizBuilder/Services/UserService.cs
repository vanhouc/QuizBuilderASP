using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QuizBuilder.Models;

namespace QuizBuilder.Services
{
    public class UserService
    {
        private QuizBuilderContext _db;

        public UserService()
        {
            _db = new QuizBuilderContext();
        }
        public bool createUser(User newUser)
        {
            try
            {
                _db.Users.Add(newUser);
                _db.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }
        public User[] getUsers()
        {
            return (from u in _db.Users
                    select u).ToArray<User>();
        }
    }
}