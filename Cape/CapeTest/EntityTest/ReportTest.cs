using System;
using Xunit;
using Cape.Models;

namespace Cape.Test.EntitiesTest
{
    public class ReportTest
    {
        [Fact]
        public void ReportsCanBeMade()
        {
            Report report = new Report();
            report.Name = "Report Name Test";
            report.ReportId = 0;
            report.UploadDate = DateTime.Today;

            Assert.Equal(report.Name, "Report Name Test");
            Assert.Equal(report.ReportId, 0);
            Assert.Equal(report.UploadDate, DateTime.Today);
        }
    }
}
