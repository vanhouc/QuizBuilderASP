using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QuizBuilder.Models;
using System.Data.Entity;

namespace QuizBuilder.Services
{
    public static class UserService
    {
        public static User AddUser(User newUser)
        {
            using (QuizBuilderContext db = new QuizBuilderContext())
            {
                User toAdd = newUser;
                db.Users.Add(toAdd);
                db.SaveChanges();
                return toAdd;
            }
        }
        public static User DeleteUser(int id)
        {
            using (QuizBuilderContext db = new QuizBuilderContext())
            {
                User toDelete = db.Users.Find(id);
                db.Users.Remove(toDelete);
                db.SaveChanges();
                return toDelete;
            }
        }
        public static User[] GetUsers()
        {
            using (QuizBuilderContext db = new QuizBuilderContext())
            {
                return (from u in db.Users
                        select u).ToArray<User>();
            }
        }
        public static User ValidateLogin(LoginModel login)
        {
            using (QuizBuilderContext db = new QuizBuilderContext())
            {
                try
                {
                    User user = db.Users.First(x => x.Username == login.Username && x.Password == login.Password);
                    return db.Users.Find(user.UserID);
                }
                catch
                {
                    return null;
                }
            }
        }
        public static void SaveChanges(User user)
        {
            using (QuizBuilderContext db = new QuizBuilderContext())
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
        }
        public static User FindUser(int id)
        {
            using (QuizBuilderContext db = new QuizBuilderContext())
            {
                return db.Users.Find(id);
            }
        }
    }
}