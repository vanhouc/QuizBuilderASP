using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuizBuilder.Models;
using QuizBuilder.Services;
using System.Linq;


namespace QuizBuilder.Test
{
    [TestClass]
    public class UserServiceTest
    {
        [TestMethod]
        public void ServiceUserTest()
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
            Assert.AreEqual(testUser, UserService.AddUser(testUser));
            Assert.IsTrue(UserService.GetUsers().Any(x => x.Username == "testuser"));
            UserService.DeleteUser(testUser.UserID);
            Assert.IsFalse(UserService.GetUsers().Any(x => x.Username == "testuser"));
        }
        [TestMethod]
        public void EditUserTest()
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
            UserService.AddUser(testUser);
            testUser.Username = "tuser";
            UserService.SaveChanges(testUser);
            using (QuizBuilderContext db = new QuizBuilderContext())
            {
                User updatedUser = db.Users.First(x => x.Username == "tuser");
                Assert.AreEqual(testUser.Username, updatedUser.Username);
            }
            UserService.DeleteUser(testUser.UserID);
            Assert.IsNull(UserService.FindUser(testUser.UserID));
        }
    }
}