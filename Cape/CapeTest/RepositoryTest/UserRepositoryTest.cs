using Moq;
using System.Data.Entity;
using Cape.Repositories;
using Cape.Models;
using Cape.Data;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cape.Test.RepositoryTest
{
    [TestClass]
    public class UserRepositoryTest
    {
        // Mock DBSet, Context, Repo, and Repo Connection we will be using in these tests
        private Mock<DbSet<ApplicationUser>> mock_user_set;
        private Mock<ApplicationDbContext> mock_context;
        private UserRepositoryConnection userRepositoryConnection;
        private UserRepository userRepository;

        //This method connects a IEnumerable of Users to the mock context. We do this at initialization.
        private void ConnectMocksToDataStore(IEnumerable<ApplicationUser> data_store)
        {
            var data_source = data_store.AsQueryable();
            mock_user_set.As<IQueryable<ApplicationUser>>().Setup(data => data.Provider).Returns(data_source.Provider);
            mock_user_set.As<IQueryable<ApplicationUser>>().Setup(data => data.Expression).Returns(data_source.Expression);
            mock_user_set.As<IQueryable<ApplicationUser>>().Setup(data => data.ElementType).Returns(data_source.ElementType);
            mock_user_set.As<IQueryable<ApplicationUser>>().Setup(data => data.GetEnumerator()).Returns(data_source.GetEnumerator());
            mock_context.Setup(u => u.Users).Returns(mock_user_set.Object);
        }

        [TestInitialize]
        public void Initialize()
        {
            //Giving Repo, Repo Connection, and Mock_context initial values at the begginning of each test
            mock_context = new Mock<ApplicationDbContext>() { CallBase = true };
            mock_user_set = new Mock<DbSet<ApplicationUser>>();
            userRepositoryConnection = new UserRepositoryConnection(mock_context.Object);
            userRepository = new UserRepository(userRepositoryConnection);

            //Populating the fake context to interact with in every test
            List<ApplicationUser> ListOfUsers = new List<ApplicationUser>();
            ConnectMocksToDataStore(ListOfUsers);

            mock_user_set.Setup(a => a.Add(It.IsAny<ApplicationUser>()))
                .Callback((ApplicationUser x) => ListOfUsers.Add(x));
        }

        [TestCleanup]
        public void Cleanup()
        {
            // Setting our context and Repos back to null after each test
            mock_context = null;
            mock_user_set = null;
            userRepository = null;
            userRepositoryConnection = null;
        }

        [TestMethod]
        public void InsureRepoCreation()
        {
            Assert.IsNotNull(userRepository);
        }

        [TestMethod]
        public void RepoCanCreateUsersAndGetByID()
        {
            ApplicationUser CreatedUser = new ApplicationUser();
            CreatedUser.FirstName = "Created FirstName";
            CreatedUser.LastName = "Created LastName";
            CreatedUser.Id = "Created Guid";
            CreatedUser.Email = "Created Email";
            CreatedUser.PasswordHash = "Created Password Hash";

            userRepository.Create(CreatedUser);

            ApplicationUser ShouldBeCreatedUser = userRepository.GetById(CreatedUser.Id);

            Assert.IsNotNull(ShouldBeCreatedUser);
            Assert.AreEqual(ShouldBeCreatedUser, CreatedUser);
        }

        [TestMethod]
        public void UserRepoCanUpdateUsers()
        {
            ApplicationUser CreatedUser = new ApplicationUser();
            CreatedUser.FirstName = "Created FirstName";
            CreatedUser.LastName = "Created LastName";
            CreatedUser.Id = "Created Guid";
            CreatedUser.Email = "Created Email";
            CreatedUser.PasswordHash = "Created Password Hash";

            userRepository.Create(CreatedUser);

            CreatedUser.FirstName = "Updated FirstName";
            CreatedUser.LastName = "Updated LastName";

            userRepository.Update(CreatedUser);

            ApplicationUser updatedUser = userRepository.GetById(CreatedUser.Id);

            Assert.AreEqual(updatedUser.FirstName, "Updated FirstName");
            Assert.AreEqual(updatedUser.LastName, "Updated LastName");
        }
    }
}
