using System;
using System.Collections.Generic;
using Cape.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cape.Test.EntitiesTest
{
    [TestClass]
    public class ReportTest
    {
        [TestMethod]
        public void ReportsCanBeMade()
        {
            Report report = new Report();
            report.UploadDate = DateTime.Today;
            report.ReportId = 1;
            report.Transactions = new List<Transaction>();
            report.UserId = "Some Guid";

            Assert.AreEqual(report.UploadDate, DateTime.Today);
        }
    }
}
