using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QuizBuilder.Models;
using System.Data.Entity;
using QuizBuilder.DataContexts;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace QuizBuilder.Services
{
    public static class UserService
    {
        public static User AddUser(User newUser)
        {
            using (QuizBuilderDb db = new QuizBuilderDb())
            {
                User toAdd = newUser;
                db.Users.Add(toAdd);
                db.SaveChanges();
                return toAdd;
            }
        }
        public static QuizUserViewModel CreateUserModel(User user)
        {
            return new QuizUserViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.Username,
                EmailAddress = user.Email
            };
        }
        public static User DeleteUser(int id)
        {
            using (QuizBuilderDb db = new QuizBuilderDb())
            {
                User toDelete = db.Users.Find(id);
                db.Users.Remove(toDelete);
                db.SaveChanges();
                return toDelete;
            }
        }
        public static User UpdateUser(User updatedUser)
        {
            using (QuizBuilderDb db = new QuizBuilderDb())
            {
                User currentUser = db.Users.Find(updatedUser.UserID);
                if (currentUser != null)
                {
                    currentUser.FirstName = updatedUser.FirstName;
                    currentUser.LastName = updatedUser.LastName;
                    currentUser.Email = updatedUser.Email;
                    db.SaveChanges();
                    return currentUser;
                }
                else
                {
                    return currentUser;
                }
            }
        }
        public static User UpdateUser(QuizUserViewModel updatedUser)
        {
            using (QuizBuilderDb db = new QuizBuilderDb())
            {
                User currentUser = UserService.FindByName(updatedUser.UserName);
                if (currentUser != null)
                {
                    currentUser = db.Users.Find(currentUser.UserID);
                    currentUser.FirstName = updatedUser.FirstName;
                    currentUser.LastName = updatedUser.LastName;
                    currentUser.Email = updatedUser.EmailAddress;
                    db.SaveChanges();
                    return currentUser;
                }
                else
                {
                    return currentUser;
                }
            }
        }

        public static User[] GetUsers()
        {
            using (QuizBuilderDb db = new QuizBuilderDb())
            {
                return (from u in db.Users
                        select u).ToArray<User>();
            }
        }

        public static User FindUser(int id)
        {
            using (QuizBuilderDb db = new QuizBuilderDb())
            {
                return db.Users.Find(id);
            }
        }
        public static User FindByName(string username)
        {
            using (QuizBuilderDb db = new QuizBuilderDb())
            {
                if (db.Users.Any(u => u.Username == username))
                {
                    User user = db.Users.First(u => u.Username == username);
                    return user;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}