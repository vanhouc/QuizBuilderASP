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
        public bool AddUser(User newUser)
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
        public bool DeleteUser(User user)
        {
            try
            {
                _db.Users.Remove(user);
                _db.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }
        public User[] GetUsers()
        {
            return (from u in _db.Users
                    select u).ToArray<User>();
        }
    }
}