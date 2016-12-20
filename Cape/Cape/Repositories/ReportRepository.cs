using System.Linq;
using Cape.Models;
using Cape.Data;
using Cape.Interfaces;
using System;
using System.Threading.Tasks;

namespace Cape.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private readonly ApplicationDbContext context;

        public ReportRepository(ReportRepositoryConnection connection)
        {
            context = connection.AppContext;
        }

        public int Create(ApplicationUser user)
        {
            Report newReport = new Report();

            newReport.User = user;

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

        public void Update(Report obj)
        {
            context.Entry(obj);

            context.ChangeTracker.DetectChanges();

            context.SaveChangesAsync();
        }
    }
}
