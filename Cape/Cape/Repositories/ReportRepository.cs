using System.Linq;
using Cape.Models;
using Cape.Data;
using Cape.Interfaces;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Data.Entity;

namespace Cape.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private readonly ApplicationDbContext context;

        public ReportRepository(ReportRepositoryConnection connection)
        {
            context = connection.AppContext;
        }

        public int Create(string UserId)
        {
            Report newReport = new Report();

            newReport.UserId = UserId;

            newReport.UploadDate = DateTime.Now;

            context.Report.Add(newReport);

            context.SaveChanges();

            return newReport.ReportId;
        }

        public Report GetById(int reportId)
        {
            Report selectedReport = context.Report.Single(r => r.ReportId == reportId);

            return selectedReport;
        }

        public List<Report> GetByUser (string UserId)
        {

            List<Report> listOfReports = context.Report.Where(r => r.UserId == UserId).ToList();

            //List<Report> listOfReports = context.Report.Include(r => r.Transactions).ToList();

            //IEnumerable<Report> listOfReports =
            //    from report in context.Report
            //    join user in context.Users on report.User equals user
            //    select report;

            //var qry = (
            //           from r in context.Report
            //           join u in context.Users on r.User.Id equals u.Id
            //           select r).Include("User").ToList();

            //List<Report> listOfReports = qry.Where(r => r.User.Id == UserId).ToList();

            //group new Report { ReportId = report.ReportId, UploadDate = report.UploadDate, User = user};


            return listOfReports;
        }

        public void Update(Report obj)
        {
            context.Entry(obj);

            context.ChangeTracker.DetectChanges();

            context.SaveChangesAsync();
        }
    }
}
