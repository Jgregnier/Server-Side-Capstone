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
        private Mock<DbSet<Category>> mock_category_set;
        private Mock<ApplicationDbContext> mock_context;
        private CategoryRepositoryConnection categoryRepositoryConnection;
        private CategoryRepository categoryRepository;

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
            mock_context = new Mock<ApplicationDbContext>() { CallBase = true};
            mock_category_set = new Mock<DbSet<Category>>();
            categoryRepositoryConnection = new CategoryRepositoryConnection(mock_context.Object);
            categoryRepository = new CategoryRepository(categoryRepositoryConnection);
        }

        [TestCleanup]
        public void Cleanup()
        {
            mock_context = null;
            mock_category_set = null;
            categoryRepository = null;
        }

        [TestMethod]
        public void InsureRepoCreation()
        {
            Assert.IsNotNull(categoryRepository);
        }

        [TestMethod]
        public void RepoCanCreateCategories()
        {

            List<Category> ListOfCategories = new List<Category>();
            
            Category TestCategory = new Category();
            TestCategory.Name = "Test Category";
            TestCategory.CategoryId = 0;

            ListOfCategories.Add(TestCategory);

            ConnectMocksToDataStore(ListOfCategories);

            mock_category_set.Setup(a => a.Add(It.IsAny<Category>()))
                .Callback((Category x) => ListOfCategories.Add(x));

            Category CreatedCategory = new Category();
            CreatedCategory.Name = "Created Category";
            CreatedCategory.CategoryId = 1;

            categoryRepository.Create(CreatedCategory);

            Category ShouldBeCreatedCategory = categoryRepository.GetById(CreatedCategory.CategoryId);

            Assert.IsNotNull(ShouldBeCreatedCategory);
            Assert.AreEqual(ShouldBeCreatedCategory, CreatedCategory);
        }
    }
}
