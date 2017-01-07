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
    public class CategoryRepositoryTest
    {
        // Mock DBSet, Context, Repo, and Repo Connection we will be using in these tests
        private Mock<DbSet<Category>> mock_category_set;
        private Mock<ApplicationDbContext> mock_context;
        private CategoryRepositoryConnection categoryRepositoryConnection;
        private CategoryRepository categoryRepository;

        //This method connects a IEnumerable of Categories to the mock context. We do this at initialization.
        private void ConnectMocksToDataStore(IEnumerable<Category> data_store)
        {
            var data_source = data_store.AsQueryable();
            mock_category_set.As<IQueryable<Category>>().Setup(data => data.Provider).Returns(data_source.Provider);
            mock_category_set.As<IQueryable<Category>>().Setup(data => data.Expression).Returns(data_source.Expression);
            mock_category_set.As<IQueryable<Category>>().Setup(data => data.ElementType).Returns(data_source.ElementType);
            mock_category_set.As< IQueryable<Category >> ().Setup(data => data.GetEnumerator()).Returns(data_source.GetEnumerator());
            mock_context.Setup(c => c.Category).Returns(mock_category_set.Object);
        }

        [TestInitialize]
        public void Initialize()
        {
            //Giving Repo, Repo Connection, and Mock_context initial values at the begginning of each test
            mock_context = new Mock<ApplicationDbContext>() { CallBase = true};
            mock_category_set = new Mock<DbSet<Category>>();
            categoryRepositoryConnection = new CategoryRepositoryConnection(mock_context.Object);
            categoryRepository = new CategoryRepository(categoryRepositoryConnection);

            //Populating the fake context to interact with in every test
            List<Category> ListOfCategories = new List<Category>();
            ConnectMocksToDataStore(ListOfCategories);

            mock_category_set.Setup(a => a.Add(It.IsAny<Category>()))
                .Callback((Category x) => ListOfCategories.Add(x));
        }

        [TestCleanup]
        public void Cleanup()
        {
            // Setting our context and Repos back to null after each test
            mock_context = null;
            mock_category_set = null;
            categoryRepository = null;
            categoryRepositoryConnection = null; 
        }

        [TestMethod]
        public void InsureRepoCreation()
        {
            Assert.IsNotNull(categoryRepository);
        }

        [TestMethod]
        public void RepoCanCreateCategoriesAndGetByID()
        {
            Category CreatedCategory = new Category();
            CreatedCategory.Name = "Created Category";
            CreatedCategory.CategoryId = 1;

            categoryRepository.Create(CreatedCategory);

            Category ShouldBeCreatedCategory = categoryRepository.GetById(CreatedCategory.CategoryId);

            Assert.IsNotNull(ShouldBeCreatedCategory);
            Assert.AreEqual(ShouldBeCreatedCategory, CreatedCategory);
        }

        [TestMethod]
        public void CategoryRepoCanUpdateCategories()
        {
            Category CreatedCategory = new Category();
            CreatedCategory.Name = "Created Category";
            CreatedCategory.CategoryId = 1;

            categoryRepository.Create(CreatedCategory);

            CreatedCategory.Name = "Updated Category Name";

            categoryRepository.Update(CreatedCategory);

            Category updatedCategory = categoryRepository.GetById(CreatedCategory.CategoryId);

            Assert.AreEqual(updatedCategory.Name, "Updated Category Name");
        }
    }
}
