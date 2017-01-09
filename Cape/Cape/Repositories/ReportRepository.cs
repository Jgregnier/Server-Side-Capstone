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
            List<Report> listOfReports = context.Report.Include(r => r.Transactions).Where(r => r.UserId == UserId).ToList();

            return listOfReports;
        }

        public void Update(Report obj)
        {
            context.Entry(obj);

            context.ChangeTracker.DetectChanges();

            context.SaveChangesAsync();
        }

        public void DeleteReport(int ReportId)
        {
            Report ReportToBeDeleted = context.Report.Where(r => r.ReportId == ReportId).Single();

            context.Report.Remove(ReportToBeDeleted);

            context.ChangeTracker.DetectChanges();

            context.SaveChangesAsync();
        }
    }
}
