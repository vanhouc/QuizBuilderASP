﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuizBuilder.Models;
using System.Data.Entity;
using System.Linq;

namespace QuizBuilder.Test
{
    [TestClass]
    public class UserModelTest
    {
        [TestMethod]
        public void CreateAndDeleteUser()
        {
            using (QuizBuilderContext db = new QuizBuilderContext())
            {
                User testUser = new User
                {
                    UserID = 0,
                    FirstName = "Test",
                    LastName = "Guy",
                    Username = "testuser",
                    Password = "123",
                    Email = "test@test.com",
                    IsAdmin = false
                };
                db.Users.Add(testUser);
                db.SaveChanges();
                Assert.IsNotNull(db.Users.Find(testUser.UserID));
                var removeTestUsers = (from u in db.Users
                                       where u.Username == "testuser"
                                       select u).ToArray();
                db.Users.RemoveRange(removeTestUsers);
                db.SaveChanges();
                Assert.IsNull(db.Users.Find(testUser.UserID));
            }
        }
    }
}