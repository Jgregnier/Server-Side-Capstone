using System;
using Cape.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cape.Test.EntitiesTest
{
    [TestClass]
    public class TransactionTest
    {
        [TestMethod]
        public void TransactionsCanBeMade()
        {
            Report report = new Report();
            report.Name = "Report Name Test";
            report.ReportId = 0;
            report.UploadDate = DateTime.Today;

            Category category = new Category();
            category.Name = "Category Name Test";
            category.CategoryId = 0;

            Transaction transaction = new Transaction();
            transaction.Name = "Transaction Name Test";
            transaction.Amount = -35.15;
            transaction.TransactionId = 0;
            transaction.Category = category;
            transaction.Report = report;

            Assert.AreEqual(transaction.Name, "Transaction Name Test");
            Assert.AreEqual(transaction.TransactionId, 0);
            Assert.AreEqual(transaction.Amount, -35.15);

            Assert.AreEqual(transaction.Category, category);
            Assert.AreEqual(transaction.Report, report);
        }
    }
}