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
        public void AddAndDeleteUserTest()
        {
            UserService userService = new UserService();
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
            Assert.IsTrue(userService.AddUser(testUser));
            Assert.IsTrue(userService.GetUsers().Any(x => x.Username == "testuser"));
            Assert.IsTrue(userService.DeleteUser(testUser));
            Assert.IsFalse(userService.GetUsers().Any(x => x.Username == "testuser"));
        }
    }
}
