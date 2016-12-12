using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cape.Models;

namespace Cape.Test.EntitiesTest
{
    [TestClass]
    public class CategoryTest
    {
        [TestMethod]
        public void CategoiresCanBeMade()
        {
            Category category = new Category();
            category.Name = "Category Name Test";
            category.CategoryId = 0;

            Assert.AreEqual(category.Name, "Category Name Test");
            Assert.AreEqual(category.CategoryId, 0);
        }
    }
}
