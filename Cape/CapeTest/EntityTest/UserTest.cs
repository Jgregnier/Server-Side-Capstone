using Cape.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cape.Test.EntitiesTest
{
    [TestClass]
    public class UserTest
    {
        [TestMethod]
        public void UsersCanBeMade()
        {
            ApplicationUser user = new ApplicationUser();
            user.FirstName = "User FirstName Test";
            user.LastName = "User LastName Test";
            user.Email = "User Email Test";
            user.PasswordHash = "User PasswordHash Test";

            Assert.AreEqual(user.FirstName, "User FirstName Test");
            Assert.AreEqual(user.LastName, "User LastName Test");
            Assert.AreEqual(user.Email, "User Email Test");
            Assert.AreEqual(user.PasswordHash, "User PasswordHash Test");
        }
    }
}