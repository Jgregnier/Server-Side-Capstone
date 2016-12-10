using System;
using Xunit;
using Cape.Models;

namespace Cape.Test.EntitiesTest
{
    public class TransactionTest
    {
        [Fact]
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

            Assert.Equal(transaction.Name, "Transaction Name Test");
            Assert.Equal(transaction.TransactionId, 0);
            Assert.Equal(transaction.Amount, -35.15);

            Assert.Equal(transaction.Category, category);
            Assert.Equal(transaction.Report, report);
        }
    }
}