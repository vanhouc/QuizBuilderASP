using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QuizBuilder.Models;
using System.Data.Entity;

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
        public User ValidateLogin(LoginModel login)
        {
            try
            {
                return db.Users.First(x => x.Username == login.Username && x.Password == login.Password);
            }
            catch
            {
                return null;
            }
        }
        public void SaveChanges(User user)
        {
            if (db.Entry(user).State == EntityState.Modified)
                db.SaveChanges();
            if (db.Entry(user).State == EntityState.Detached)
            {
                db.Users.Attach(user);
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
            }
            if (db.Entry(user).State == EntityState.Unchanged)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
            }

        }
        public void ReloadUser(User user)
        {
            db.Entry(user).Reload();
        }
        public User FindUser(int id)
        {
            return db.Users.Find(id);
        }
    }
}