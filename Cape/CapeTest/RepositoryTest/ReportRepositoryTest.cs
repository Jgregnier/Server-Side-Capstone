﻿using Moq;
using System.Data.Entity;
using Cape.Repositories;
using Cape.Models;
using Cape.Data;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Cape.Test.RepositoryTest
{
    [TestClass]
    public class ReportRepositoryTest
    {
        // Mock DBSet, Context, Repo, and Repo Connection we will be using in these tests
        private Mock<DbSet<Report>> mock_report_set;
        private Mock<ApplicationDbContext> mock_context;
        private ReportRepositoryConnection reportRepositoryConnection;
        private ReportRepository reportRepository;

        //This method connects a IEnumerable of Reports to the mock context. We do this at initialization.
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
            //Giving Repo, Repo Connection, and Mock_context initial values at the begginning of each test
            mock_context = new Mock<ApplicationDbContext>() { CallBase = true };
            mock_report_set = new Mock<DbSet<Report>>();
            reportRepositoryConnection = new ReportRepositoryConnection(mock_context.Object);
            reportRepository = new ReportRepository(reportRepositoryConnection);

            //Populating the fake context to interact with in every test
            List<Report> ListOfReports = new List<Report>();
            ConnectMocksToDataStore(ListOfReports);

            mock_report_set.Setup(a => a.Add(It.IsAny<Report>()))
                .Callback((Report x) => ListOfReports.Add(x));
        }

        [TestCleanup]
        public void Cleanup()
        {
            // Setting our context and Repos back to null after each test
            mock_context = null;
            mock_report_set = null;
            reportRepository = null;
            reportRepositoryConnection = null;
        }

        [TestMethod]
        public void InsureRepoCreation()
        {
            Assert.IsNotNull(reportRepository);
        }

        [TestMethod]
        public void RepoCanCreateReportAndGetByID()
        {
            ApplicationUser fakeUser = new ApplicationUser();
            fakeUser.FirstName = "Test First Name";
            fakeUser.LastName = "Test Last Name";
            fakeUser.Id = "Test Guid";

            int CreatedReportId = reportRepository.Create(fakeUser.Id);

            Report ShouldBeCreatedReport = reportRepository.GetById(CreatedReportId);

            Assert.IsNotNull(ShouldBeCreatedReport);
            Assert.AreEqual(ShouldBeCreatedReport.ReportId, CreatedReportId);
        }

        [TestMethod]
        public void RepoCanGetAllReportsByUser()
        {
            ApplicationUser fakeUser = new ApplicationUser();
            fakeUser.FirstName = "Test First Name";
            fakeUser.LastName = "Test Last Name";
            fakeUser.Id = "Test Guid";

            int FirstFakeReportId = reportRepository.Create(fakeUser.Id);

            int SecondFakeReportId = reportRepository.Create(fakeUser.Id);



            List<Report> shouldBeBothOfTheCreatedReports = reportRepository.GetByUser(fakeUser.Id);

            Assert.AreEqual(FirstFakeReportId, shouldBeBothOfTheCreatedReports[0].ReportId);
            Assert.AreEqual(shouldBeBothOfTheCreatedReports[0].UserId, "Test Guid");

            Assert.AreEqual(SecondFakeReportId, shouldBeBothOfTheCreatedReports[1].ReportId);
            Assert.AreEqual(shouldBeBothOfTheCreatedReports[1].UserId, "Test Guid");

        }

        [TestMethod]
        public void ReportRepoCanUpdateReports()
        {
            ApplicationUser fakeUser = new ApplicationUser();
            fakeUser.FirstName = "Test First Name";
            fakeUser.LastName = "Test Last Name";
            fakeUser.Id = "Test Guid";

            int CreatedReportId = reportRepository.Create(fakeUser.Id);

            Report CreatedReport = reportRepository.GetById(CreatedReportId);

            CreatedReport.UploadDate = new DateTime(2015, 11, 21);

            reportRepository.Update(CreatedReport);

            Report updatedReport = reportRepository.GetById(CreatedReport.ReportId);

            Assert.AreEqual(updatedReport.ReportId, CreatedReportId);
            Assert.AreEqual(updatedReport.UploadDate, new DateTime(2015, 11, 21));
        }

        [TestMethod]
        public void ReportRepoCanDeleteReports()
        {
            ApplicationUser fakeUser = new ApplicationUser();
            fakeUser.FirstName = "Test First Name";
            fakeUser.LastName = "Test Last Name";
            fakeUser.Id = "Test Guid";

            int CreatedReportId = reportRepository.Create(fakeUser.Id);

            Report CreatedReport = reportRepository.GetById(CreatedReportId);

            Assert.IsNotNull(CreatedReport);

            reportRepository.DeleteReport(CreatedReport.ReportId);

            Report DeletedReport = reportRepository.GetById(CreatedReportId);

            Assert.IsNull(DeletedReport);
        }
    }
}
