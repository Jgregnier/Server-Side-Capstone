using System.Data.Entity;
using Moq;
using Cape.Repositories;
using Cape.Models;
using Cape.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Cape.Test.RepositoryTest
{
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

        public void Initialize()
        {
            mock_context = new Mock<ApplicationDbContext>();
            mock_category_set = new Mock<DbSet<Category>>();
            categoryRepositoryConnection = new CategoryRepositoryConnection(mock_context.Object);
            categoryRepository = new CategoryRepository(categoryRepositoryConnection);
        }

        public void Cleanup()
        {
            mock_context = null;
            mock_category_set = null;
            categoryRepository = null;
        }

        [Fact]
        public void InsureRepoCreation()
        {
            Initialize();

            Assert.NotNull(categoryRepository);

            Cleanup();
        }

        [Fact]
        public void RepoCanCreateCategories()
        {
            Initialize();

            List<Category> ListOfCategories = new List<Category>();

            ConnectMocksToDataStore(ListOfCategories);

            Category TestCategory = new Category();
            TestCategory.Name = "Test Category";
            TestCategory.CategoryId = 0;

            mock_category_set.Setup(a => a.Add(It.IsAny<Category>()))
                .Callback((Category x) => ListOfCategories.Add(x))
                .Returns(mock_category_set.Object.Where(a => a.Name == "Test Category").Single());

            categoryRepository.Create(TestCategory);

            Category ShouldBeTestCategory = categoryRepository.GetById(TestCategory.CategoryId);

            Assert.NotNull(ShouldBeTestCategory);
            Assert.Equal(ShouldBeTestCategory, TestCategory);

            Cleanup();
        }
    }
}
