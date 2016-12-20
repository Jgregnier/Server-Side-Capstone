using Moq;
using System.Data.Entity;
using Cape.Repositories;
using Cape.Models;
using Cape.Data;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.AspNet.Identity;

namespace Cape.Test.RepositoryTest
{
    [TestClass]
    public class ReportRepositoryTest
    {
        private Mock<DbSet<Report>> mock_report_set;
        private Mock<ApplicationDbContext> mock_context;
        private ReportRepositoryConnection reportRepositoryConnection;
        private ReportRepository reportRepository;

        private void ConnectMocksToDataStore(IEnumerable<Report> data_store)
        {
            var data_source = data_store.AsQueryable();
            mock_report_set.As<IQueryable<Report>>().Setup(data => data.Provider).Returns(data_source.Provider);
            mock_report_set.As<IQueryable<Report>>().Setup(data => data.Expression).Returns(data_source.Expression);
            mock_report_set.As<IQueryable<Report>>().Setup(data => data.ElementType).Returns(data_source.ElementType);
            mock_report_set.As<IQueryable<Report>>().Setup(data => data.GetEnumerator()).Returns(data_source.GetEnumerator());
            mock_context.Setup(r => r.Report).Returns(mock_report_set.Object);
        }

        [TestInitialize]
        public void Initialize()
        {
            mock_context = new Mock<ApplicationDbContext>() { CallBase = true };
            mock_report_set = new Mock<DbSet<Report>>();
            reportRepositoryConnection = new ReportRepositoryConnection(mock_context.Object);
            reportRepository = new ReportRepository(reportRepositoryConnection);
        }

        [TestCleanup]
        public void Cleanup()
        {
            mock_context = null;
            mock_report_set = null;
            reportRepository = null;
        }

        [TestMethod]
        public void InsureRepoCreation()
        {
            Assert.IsNotNull(reportRepository);
        }

        [TestMethod]
        public void RepoCanCreateReportAndGetByID()
        {

            List<Report> ListOfReports = new List<Report>();


            ConnectMocksToDataStore(ListOfReports);

            mock_report_set.Setup(a => a.Add(It.IsAny<Report>()))
                .Callback((Report x) => ListOfReports.Add(x));

            ApplicationUser fakeUser = new ApplicationUser();
            fakeUser.FirstName = "Test First Name";
            fakeUser.LastName = "Test Last Name";

            int CreatedReportId = reportRepository.Create(fakeUser);

            Report ShouldBeCreatedReport = reportRepository.GetById(CreatedReportId);

            Assert.IsNotNull(ShouldBeCreatedReport);
            Assert.AreEqual(ShouldBeCreatedReport.ReportId, CreatedReportId);
        }

        [TestMethod]
        public void ReportRepoCanUpdateReports()
        {
            List<Report> ListOfReports = new List<Report>();

            ConnectMocksToDataStore(ListOfReports);

            mock_report_set.Setup(a => a.Add(It.IsAny<Report>()))
                .Callback((Report x) => ListOfReports.Add(x));

            ApplicationUser fakeUser = new ApplicationUser();
            fakeUser.FirstName = "Test First Name";
            fakeUser.LastName = "Test Last Name";

            int CreatedReportId = reportRepository.Create(fakeUser);

            Report CreatedReport = reportRepository.GetById(CreatedReportId);

            CreatedReport.UploadDate = new DateTime(2015, 11, 21);

            reportRepository.Update(CreatedReport);

            Report updatedReport = reportRepository.GetById(CreatedReport.ReportId);

            Assert.AreEqual(updatedReport.ReportId, CreatedReportId);
            Assert.AreEqual(updatedReport.UploadDate, new DateTime(2015, 11, 21));
        }
    }
}
