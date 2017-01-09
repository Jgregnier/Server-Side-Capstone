using Moq;
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
            fakeUser.Id = "Test User Guid";

            //Our mock database does not auto increment ReportId's like our MSSql DB does. We therefore have to retrieve
            //it, add an incremented ID, then update the Report.

            //First Fake Report to test
            int FirstFakeReportId = reportRepository.Create(fakeUser.Id);
            Report FirstFakeReport = reportRepository.GetById(0);
            FirstFakeReport.ReportId = 1;
            reportRepository.Update(FirstFakeReport);

            //Second Fake Report to test
            int SecondFakeReportId = reportRepository.Create(fakeUser.Id);
            Report SecondFakeReport = reportRepository.GetById(0);
            SecondFakeReport.ReportId = 2;
            reportRepository.Update(SecondFakeReport);

            //Due to the .Include on the method GetByUser in the Report Repository, this method is currently untestable.
            //In the application it is currently working, however, if something were to break, go to the report repo and 
            //remove the include to test ability to retrieve reports by the users Id.
            //Instead of testing that, we will test that reports are being saved with the user Id successfully. 

                //List<Report> shouldBeBothOfTheCreatedReports = reportRepository.GetByUser(fakeUser.Id);

                //Assert.AreEqual(FirstFakeReportId, shouldBeBothOfTheCreatedReports[0].ReportId);
                //Assert.AreEqual(shouldBeBothOfTheCreatedReports[0].UserId, "Test User Guid");

                //Assert.AreEqual(SecondFakeReportId, shouldBeBothOfTheCreatedReports[1].ReportId);
                //Assert.AreEqual(shouldBeBothOfTheCreatedReports[1].UserId, "Test User Guid");

            //IF INCLUDE IS STILL IN THE REPORT REPO GetByUserId Method DO NOT COMMENT OUT ABOVE INDENTED TEST

            Report FirstReport = reportRepository.GetById(1);
            Report SecondReport = reportRepository.GetById(2);

            Assert.AreEqual(FirstReport.UserId, "Test User Guid");
            Assert.AreEqual(SecondReport.UserId, "Test User Guid");

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

        //Current context does not support removing?
        //Until further knowledge of the Moq framework I wont be able to test the deletability of Reports.
        //Reports are being deleted in the application successfully as of 1/9/2017

        //[TestMethod]
        //public void ReportRepoCanDeleteReports()
        //{
        //    ApplicationUser fakeUser = new ApplicationUser();
        //    fakeUser.FirstName = "Test First Name";
        //    fakeUser.LastName = "Test Last Name";
        //    fakeUser.Id = "Test Guid";

        //    //Our mock database does not auto increment ReportId's like our MSSql DB does. We therefore have to retrieve
        //    //it, add an incremented ID, then update the Report.
        //    int CreatedReportId = reportRepository.Create(fakeUser.Id);
        //    Report FakeReport = reportRepository.GetById(0);
        //    FakeReport.ReportId = 1;
        //    reportRepository.Update(FakeReport);

        //    Report CreatedReport = reportRepository.GetById(1);

        //    Assert.IsNotNull(CreatedReport);

        //    reportRepository.DeleteReport(1);

        //    Report DeletedReport = reportRepository.GetById(1);

        //    Assert.IsNull(DeletedReport);
        //}
    }
}
