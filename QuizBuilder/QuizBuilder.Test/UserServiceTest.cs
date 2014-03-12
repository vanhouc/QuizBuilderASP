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
            UserService userService = UserService.Instance;
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
            Assert.AreEqual(testUser, userService.AddUser(testUser));
            Assert.IsTrue(userService.GetUsers().Any(x => x.Username == "testuser"));
            userService.DeleteUser(testUser);
            Assert.IsFalse(userService.GetUsers().Any(x => x.Username == "testuser"));
        }
        [TestMethod]
        public void EditUserTest()
        {
            UserService userService = UserService.Instance;
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
            userService.AddUser(testUser);
            testUser.Username = "tuser";
            userService.SaveChanges(testUser);
            userService.ReloadUser(testUser);
            using (QuizBuilderContext db = new QuizBuilderContext())
            {
                User updatedUser = db.Users.First(x => x.Username == "tuser");
                Assert.AreEqual(testUser.Username, updatedUser.Username);
            }
            userService.DeleteUser(testUser);
            Assert.IsNull(userService.FindUser(testUser.UserID));
        }
    }
}