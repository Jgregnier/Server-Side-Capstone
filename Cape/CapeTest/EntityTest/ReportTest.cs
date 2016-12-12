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
            report.Name = "Report Name Test";
            report.ReportId = 0;
            report.UploadDate = DateTime.Today;

            Assert.AreEqual(report.Name, "Report Name Test");
            Assert.AreEqual(report.ReportId, 0);
            Assert.AreEqual(report.UploadDate, DateTime.Today);
        }
    }
}
