using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WorkshopApp;
using WorkshopApp.Controllers;
using WorkshopApp.Models;

namespace UnitTestWorkshopApp
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void SignIn_ValidCredentials_ReturnsUser()
        {
            string validLogin = "yhhlll1";
            string validPassword = "12345678";

            User result = UserController.SignIn(validLogin, validPassword);

            Assert.IsNotNull(result);
            Assert.AreEqual(validLogin, result.Login);
            Assert.AreEqual(result, App.authUser);
        }

        [TestMethod]
        public void SignIn_InvalidCredentials_ThrowsException()
        {
            string invalidLogin = "invalid";
            string invalidPassword = "invalid";

            Assert.ThrowsException<Exception>(() => UserController.SignIn(invalidLogin, invalidPassword));
        }
    }
}
