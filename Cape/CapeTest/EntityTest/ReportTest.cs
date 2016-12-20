using System;
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

            Assert.AreEqual(report.UploadDate, DateTime.Today);
        }
    }
}
