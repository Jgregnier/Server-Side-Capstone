using Xunit;
using Cape.Models;

namespace Cape.Test.EntitiesTest
{
    public class CategoryTest
    {
        [Fact]
        public void CategoiresCanBeMade()
        {
            Category category = new Category();
            category.Name = "Category Name Test";
            category.CategoryId = 0;

            Assert.Equal(category.Name, "Category Name Test");
            Assert.Equal(category.CategoryId, 0);
        }
    }
}
