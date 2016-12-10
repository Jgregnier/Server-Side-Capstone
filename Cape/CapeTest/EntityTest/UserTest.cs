using Xunit;
using Cape.Models;

namespace Cape.Test.EntitiesTest
{
    public class UserTest
    {
        [Fact]
        public void UsersCanBeMade()
        {
            ApplicationUser user = new ApplicationUser();
            user.FirstName = "User FirstName Test";
            user.LastName = "User LastName Test";
            user.Email = "User Email Test";
            user.PasswordHash = "User PasswordHash Test";

            Assert.Equal(user.FirstName, "User FirstName Test");
            Assert.Equal(user.LastName, "User LastName Test");
            Assert.Equal(user.Email, "User Email Test");
            Assert.Equal(user.PasswordHash, "User PasswordHash Test");
        }
    }
}