using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QuizBuilder.Models;

namespace QuizBuilder.Services
{
    public sealed class UserService
    {
        private static readonly UserService instance = new UserService();

        private UserService()
        {
            db = new QuizBuilderContext();
        }

        public static UserService Instance
        {
            get
            {
                return instance;
            }
        }
        private QuizBuilderContext db;

        public User AddUser(User newUser)
        {
            User toAdd = newUser;
            db.Users.Add(toAdd);
            db.SaveChanges();
            return toAdd;
        }
        public User DeleteUser(User user)
        {
            User toDelete = user;
            db.Users.Remove(toDelete);
            db.SaveChanges();
            return toDelete;
        }
        public User[] GetUsers()
        {
            return (from u in db.Users
                    select u).ToArray<User>();
        }
    }
}